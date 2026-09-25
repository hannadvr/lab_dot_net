using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinFormsApp1.lab2;

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms[0];
            form.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var calc = new Calculation_abc(
                    Convert.ToInt32(textBox1.Text),
                    Convert.ToInt32(textBox2.Text),
                    Convert.ToInt32(textBox3.Text)
                );

                label5.Text = calc.D().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var calc = new Calculation_ab(
                    Convert.ToInt32(textBox4.Text),
                    Convert.ToInt32(textBox5.Text)
                );

                label9.Text = calc.Product().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var polygon = new RegularPolygon(
                    Convert.ToInt32(textBox6.Text),
                    Convert.ToDouble(textBox7.Text)
                );

                label17.Text = polygon.CalculateArea().ToString("F2");
                label15.Text = polygon.GetCircumscribedRadius().ToString("F2");
                label16.Text = polygon.GetInscribedRadius().ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
