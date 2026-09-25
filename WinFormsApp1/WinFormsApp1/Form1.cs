namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //створення об'єкту класу табулювання
            Tabul tabul = new Tabul();
            double xn, xk, h, a;
            xn = Convert.ToDouble(this.textBox1.Text);
            xk = Convert.ToDouble(this.textBox2.Text);
            h = Convert.ToDouble(this.textBox3.Text);
            a = Convert.ToDouble(this.textBox4.Text);
            //очищуємо грід та графік
            dataGridView1.Rows.Clear();
            chart1.Series[0].Points.Clear();
            tabul.tab(xn, xk, h, a);
            //заносимо дані у грід та на графік
            for (int i = 0; i < tabul.n; i++)
            {
                //округлюємо дані до 2-х знаків після коми
                dataGridView1.Rows.Add(Math.Round(tabul.xy[i, 0], 2).ToString(),
                    Math.Round(tabul.xy[i, 1], 3).ToString());
                chart1.Series[0].Points.AddXY(tabul.xy[i, 0], tabul.xy[i, 1]);
            }
        }

        private void одновимірніМасивиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void двовимірніМасивиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            this.Hide();
        }

        private void unitTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
            this.Hide();
        }
    }
}
