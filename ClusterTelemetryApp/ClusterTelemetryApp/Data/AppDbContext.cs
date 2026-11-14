using ClusterTelemetryApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClusterTelemetryApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<ClusterEntity> Clusters => Set<ClusterEntity>();
    public DbSet<NodeEntity> Nodes => Set<NodeEntity>();
    public DbSet<MetricEntity> Metrics => Set<MetricEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Сутність ClusterEntity
        modelBuilder.Entity<ClusterEntity>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.DatacenterRegion).IsRequired().HasMaxLength(64);
            entity.Property(c => c.ClusterName).IsRequired().HasMaxLength(128);

            // Складений унікальний індекс
            entity.HasIndex(c => new { c.DatacenterRegion, c.ClusterName }).IsUnique();
        });

        // Сутність NodeEntity 
        modelBuilder.Entity<NodeEntity>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.NodeIp).IsRequired().HasMaxLength(45);
            entity.Property(n => n.OsVersion).IsRequired().HasMaxLength(64);

            // Унікальний індекс
            entity.HasIndex(n => n.NodeIp).IsUnique();

            entity.HasOne(n => n.Cluster)
                  .WithMany(c => c.Nodes)
                  .HasForeignKey(n => n.ClusterId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Сутність MetricEntity
        modelBuilder.Entity<MetricEntity>(entity =>
        {
            entity.HasKey(m => m.Id);

            // Індекси
            entity.HasIndex(m => new { m.NodeId, m.Timestamp });
            entity.HasIndex(m => m.CpuLoadPercent);

            entity.HasOne(m => m.Node)
                  .WithMany(n => n.Metrics)
                  .HasForeignKey(m => m.NodeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}