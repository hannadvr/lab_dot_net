using System.IO;
using System.Windows;
using ClusterTelemetryApp.Analytics;
using ClusterTelemetryApp.Data;
using ClusterTelemetryApp.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace ClusterTelemetryApp;

public partial class MainWindow : Window
{
    private readonly string _dbPath;
    private CancellationTokenSource? _cts;

    public MainWindow()
    {
        InitializeComponent();

        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _dbPath = Path.Combine(baseDir, "telemetry.db");

        string defaultJsonPath = Path.Combine(baseDir, "telemetry.json");
        if (File.Exists(defaultJsonPath))
        {
            FilePathTextBox.Text = defaultJsonPath;
        }

        Loaded += MainWindow_Loaded;
    }

    private AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={_dbPath}")
            .Options;
        return new AppDbContext(options);
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await using var context = CreateDbContext();
        await context.Database.EnsureCreatedAsync();

        if (!await context.Clusters.AnyAsync() && File.Exists(FilePathTextBox.Text))
        {
            await RunImportAsync(FilePathTextBox.Text);
        }
        else if (await context.Clusters.AnyAsync())
        {
            await LoadClusterSummaryAsync();
        }
    }

    private void BrowseButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "JSON файли (*.json)|*.json|Усі файли (*.*)|*.*",
            Title = "Оберіть файл телеметрії кластера"
        };

        if (dialog.ShowDialog() == true)
        {
            FilePathTextBox.Text = dialog.FileName;
            StatusTextBlock.Text = $"Обрано файл: {Path.GetFileName(dialog.FileName)}";
        }
    }

    private async void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        string path = FilePathTextBox.Text;
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            MessageBox.Show("Будь ласка, оберіть існуючий JSON-файл.", "Помилка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        await RunImportAsync(path);
    }

    private async Task RunImportAsync(string jsonPath)
    {
        _cts = new CancellationTokenSource();
        ImportButton.IsEnabled = false;
        CancelButton.IsEnabled = true;
        StatusTextBlock.Text = "Виконується потоковий асинхронний імпорт у SQLite...";

        try
        {
            await using var context = CreateDbContext();
            var reader = new JsonFileReader();
            var initializer = new DbInitializer(context, reader);

            int importedCount = await initializer.ImportFromJsonAsync(
                jsonPath,
                clearExisting: true,
                cancellationToken: _cts.Token);

            StatusTextBlock.Text = $"Імпорт завершено! Завантажено метрик у БД: {importedCount}.";
            await LoadClusterSummaryAsync();
        }
        catch (OperationCanceledException)
        {
            StatusTextBlock.Text = "Операцію імпорту скасовано користувачем.";
        }
        catch (Exception ex)
        {
            StatusTextBlock.Text = "Помилка під час імпорту.";
            MessageBox.Show($"Помилка імпорту даних:\n{ex.Message}", "Помилка БД",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            ImportButton.IsEnabled = true;
            CancelButton.IsEnabled = false;
            _cts?.Dispose();
            _cts = null;
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
    }

    private async void ShowCpuAnomalies_Click(object sender, RoutedEventArgs e)
    {
        await using var context = CreateDbContext();
        var analytics = new TelemetryAnalyticsService(context);
        var data = await analytics.GetCpuAnomaliesAsync(85.0);

        ReportTitleTextBlock.Text = $"1. Виявлені CPU-аномалії (> 85%)";
        AnalyticsDataGrid.ItemsSource = data;
        StatusTextBlock.Text = "Відображено звіт про критичне навантаження CPU.";
    }

    private async void ShowClusterSummary_Click(object sender, RoutedEventArgs e)
    {
        await LoadClusterSummaryAsync();
    }

    private async Task LoadClusterSummaryAsync()
    {
        await using var context = CreateDbContext();
        var analytics = new TelemetryAnalyticsService(context);
        var data = await analytics.GetClusterSummaryStatisticsAsync();

        ReportTitleTextBlock.Text = "2. Підсумкова статистика по кластерах і дата-центрах";
        AnalyticsDataGrid.ItemsSource = data;
        StatusTextBlock.Text = $"Відображено зведену статистику для кластерів.";
    }

    private async void ShowOsPerformance_Click(object sender, RoutedEventArgs e)
    {
        await using var context = CreateDbContext();
        var analytics = new TelemetryAnalyticsService(context);
        var data = await analytics.GetOsVersionPerformanceAsync(85.0);

        ReportTitleTextBlock.Text = "3. Порівняльний аналіз продуктивності за версіями ОС";
        AnalyticsDataGrid.ItemsSource = data;
        StatusTextBlock.Text = "Відображено статистику в розрізі операційних систем.";
    }

    private async void ShowNodeRisk_Click(object sender, RoutedEventArgs e)
    {
        await using var context = CreateDbContext();
        var analytics = new TelemetryAnalyticsService(context);
        var data = await analytics.GetHighRiskNodesRankingAsync(85.0);

        ReportTitleTextBlock.Text = "4. Рейтинг серверних нод за частотою виникнення аномалій";
        AnalyticsDataGrid.ItemsSource = data;
        StatusTextBlock.Text = "Відображено рейтинг проблемних нод.";
    }

    private async void ShowAllMetrics_Click(object sender, RoutedEventArgs e)
    {
        await using var context = CreateDbContext();
        var repo = new SqliteTelemetryRepository(context);
        var metrics = await repo.GetAllMetricsWithNodesAsync();

        var flatView = metrics.Select(m => new
        {
            m.Id,
            Region = m.Node.Cluster.DatacenterRegion,
            Cluster = m.Node.Cluster.ClusterName,
            m.Node.NodeIp,
            m.Node.OsVersion,
            m.Timestamp,
            m.CpuLoadPercent,
            m.RamUsedMb
        }).ToList();

        ReportTitleTextBlock.Text = $"Повний журнал телеметрії у БД ({flatView.Count} записів)";
        AnalyticsDataGrid.ItemsSource = flatView;
        StatusTextBlock.Text = "Відображено всі записи телеметрії з SQLite.";
    }
}