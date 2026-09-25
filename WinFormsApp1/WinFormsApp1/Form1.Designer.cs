namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox1 = new TextBox();
            label1 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            x = new DataGridViewTextBoxColumn();
            y = new DataGridViewTextBoxColumn();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            pictureBox1 = new PictureBox();
            menuStrip1 = new MenuStrip();
            роботаЗМасивамиToolStripMenuItem = new ToolStripMenuItem();
            одновимірніМасивиToolStripMenuItem = new ToolStripMenuItem();
            двовимірніМасивиToolStripMenuItem = new ToolStripMenuItem();
            unitTestToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 42);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 0;
            textBox1.Text = "2,45";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 24);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 1;
            label1.Text = "xn";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(135, 42);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 2;
            textBox2.Text = "25,2";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(260, 42);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 3;
            textBox3.Text = "0,1";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(385, 42);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 4;
            textBox4.Text = "5";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(145, 24);
            label2.Name = "label2";
            label2.Size = new Size(18, 15);
            label2.TabIndex = 5;
            label2.Text = "xk";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(270, 24);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 6;
            label3.Text = "h";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(396, 24);
            label4.Name = "label4";
            label4.Size = new Size(13, 15);
            label4.TabIndex = 7;
            label4.Text = "a";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonFace;
            button1.Location = new Point(321, 82);
            button1.Name = "button1";
            button1.Size = new Size(88, 23);
            button1.TabIndex = 8;
            button1.Text = "Обрахувати";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { x, y });
            dataGridView1.Location = new Point(12, 82);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(300, 356);
            dataGridView1.TabIndex = 9;
            // 
            // x
            // 
            x.HeaderText = "x";
            x.Name = "x";
            // 
            // y
            // 
            y.HeaderText = "y";
            y.Name = "y";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Location = new Point(318, 123);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(569, 315);
            chart1.TabIndex = 10;
            chart1.Text = "chart1";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(592, 27);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(203, 90);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { роботаЗМасивамиToolStripMenuItem, unitTestToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(899, 24);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";
            // 
            // роботаЗМасивамиToolStripMenuItem
            // 
            роботаЗМасивамиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { одновимірніМасивиToolStripMenuItem, двовимірніМасивиToolStripMenuItem });
            роботаЗМасивамиToolStripMenuItem.Name = "роботаЗМасивамиToolStripMenuItem";
            роботаЗМасивамиToolStripMenuItem.Size = new Size(125, 20);
            роботаЗМасивамиToolStripMenuItem.Text = "Робота з масивами";
            // 
            // одновимірніМасивиToolStripMenuItem
            // 
            одновимірніМасивиToolStripMenuItem.Name = "одновимірніМасивиToolStripMenuItem";
            одновимірніМасивиToolStripMenuItem.Size = new Size(189, 22);
            одновимірніМасивиToolStripMenuItem.Text = "Одновимірні масиви";
            одновимірніМасивиToolStripMenuItem.Click += одновимірніМасивиToolStripMenuItem_Click;
            // 
            // двовимірніМасивиToolStripMenuItem
            // 
            двовимірніМасивиToolStripMenuItem.Name = "двовимірніМасивиToolStripMenuItem";
            двовимірніМасивиToolStripMenuItem.Size = new Size(189, 22);
            двовимірніМасивиToolStripMenuItem.Text = "Двовимірні масиви";
            двовимірніМасивиToolStripMenuItem.Click += двовимірніМасивиToolStripMenuItem_Click;
            // 
            // unitTestToolStripMenuItem
            // 
            unitTestToolStripMenuItem.Name = "unitTestToolStripMenuItem";
            unitTestToolStripMenuItem.Size = new Size(65, 20);
            unitTestToolStripMenuItem.Text = "Unit Test";
            unitTestToolStripMenuItem.Click += unitTestToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(899, 450);
            Controls.Add(pictureBox1);
            Controls.Add(chart1);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Головна сторінка";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn x;
        private DataGridViewTextBoxColumn y;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private PictureBox pictureBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem роботаЗМасивамиToolStripMenuItem;
        private ToolStripMenuItem одновимірніМасивиToolStripMenuItem;
        private ToolStripMenuItem двовимірніМасивиToolStripMenuItem;
        private ToolStripMenuItem unitTestToolStripMenuItem;
    }
}
