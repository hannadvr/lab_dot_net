namespace WinFormsApp1
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            button2 = new Button();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(336, 307);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(369, 21);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 1;
            label1.Text = "N =";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(369, 50);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 2;
            label2.Text = "M =";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(402, 18);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 3;
            textBox1.Text = "5";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(402, 47);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 4;
            textBox2.Text = "3";
            // 
            // button1
            // 
            button1.Location = new Point(369, 91);
            button1.Name = "button1";
            button1.Size = new Size(92, 24);
            button1.TabIndex = 5;
            button1.Text = "Розрахунок";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(369, 144);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 6;
            label3.Text = "Кратні 5:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(368, 172);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 7;
            // 
            // button2
            // 
            button2.Location = new Point(506, 296);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "Закрити";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(558, 144);
            label5.Name = "label5";
            label5.Size = new Size(106, 15);
            label5.TabIndex = 9;
            label5.Text = "Сума по діагоналі";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(731, 331);
            Controls.Add(label5);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "Form3";
            Text = "Двовимірні масиви";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Label label3;
        private Label label4;
        private Button button2;
        private Label label5;
    }
}