using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Matematik II");
            comboBox1.Items.Add("Fizik II");
            comboBox1.Items.Add("Görsel Programlama");
            comboBox1.Items.Add("Programlamaya Giriş II");
            comboBox1.Items.Add("Lineer Cebir ve Diferansiyel Denklemler");
            comboBox1.Items.Add("Türk Dili II");
            comboBox1.Items.Add("Atatürk İlkeleri ve İnkılap Tarihi II");
            comboBox1.Items.Add("Development of Reading and Writing Skills II");


        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir ders seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Program.SecilenDers = comboBox1.SelectedItem.ToString();

            Form3 Form3 = new Form3();
            this.Hide();
            Form3.ShowDialog();
            this.Close();
        }
    }
}
