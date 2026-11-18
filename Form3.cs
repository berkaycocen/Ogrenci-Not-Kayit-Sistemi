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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = "Aktif Ders: " + Program.SecilenDers;

            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;

            radioButton3.Checked = true;



            GridGuncelle();
            timer1.Start();

            IstatistikHesapla();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumara.Text) || string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Öğrenci bilgileri boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int vize, final;


            try
            {
                vize = Convert.ToInt32(txtVize.Text);
                final = Convert.ToInt32(txtFinal.Text);

                if (vize < 0 || vize > 100 || final < 0 || final > 100)
                {
                    MessageBox.Show("Notlar 0 ile 100 arasında olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen not alanlarına geçerli sayısal değerler giriniz!", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error); // [cite: 18]
                return;
            }

            Ogrenci yeniOgrenci = new Ogrenci
            {
                Numara = txtNumara.Text,
                AdSoyad = txtAdSoyad.Text,
                DersAdi = Program.SecilenDers,
                VizeNotu = vize,
                FinalNotu = final
            };

            yeniOgrenci.OrtalamaHesapla();
            Program.OgrenciListesi.Add(yeniOgrenci);
            Program.VerileriKaydet();

            MessageBox.Show("Öğrenci başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtNumara.Clear();
            txtAdSoyad.Clear();
            txtVize.Clear();
            txtFinal.Clear();

            GridGuncelle();
            FiltreleVeListele();
            IstatistikHesapla();
        }
        private void GridGuncelle()
        {

            var filtrelenmisListe = Program.OgrenciListesi.Where(x => x.DersAdi == Program.SecilenDers).ToList();

            dgvOgrenciler.DataSource = null;
            dgvOgrenciler.DataSource = filtrelenmisListe;
        }

        private void FiltreleVeListele()
        {

            var dersOgrencileri = Program.OgrenciListesi.Where(x => x.DersAdi == Program.SecilenDers).ToList();

            if (radioButton3.Checked)
            {
                dgvOgrenciler.DataSource = null;
                dgvOgrenciler.DataSource = dersOgrencileri;
            }
            else if (radioButton1.Checked)
            {

                var gecenler = dersOgrencileri.Where(x => x.HarfNotu != "FF").ToList();
                dgvOgrenciler.DataSource = null;
                dgvOgrenciler.DataSource = gecenler;
            }
            else if (radioButton2.Checked)
            {

                var kalanlar = dersOgrencileri.Where(x => x.HarfNotu == "FF").ToList();
                dgvOgrenciler.DataSource = null;
                dgvOgrenciler.DataSource = kalanlar;
            }
            dgvOgrenciler.Refresh();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            FiltreleVeListele();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            FiltreleVeListele();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            FiltreleVeListele();
        }

        private void IstatistikHesapla()
        {
            var dersOgrencileri = Program.OgrenciListesi.Where(x => x.DersAdi == Program.SecilenDers).ToList();

            if (dersOgrencileri.Count == 0)
            {
                İstatistik.Items.Clear();
                İstatistik.Items.Add("Sınıf Mevcudu: 0");
                İstatistik.Items.Add("Sınıf Ortalaması: 0.00");
                İstatistik.Items.Add("Geçen Öğrenci Sayısı: 0");
                İstatistik.Items.Add("Kalan Öğrenci Sayısı: 0");
                return;
            }

            double toplamOrtalama = 0;
            int gecenSayisi = 0;
            int kalanSayisi = 0;

            foreach (var ogr in dersOgrencileri)
            {
                toplamOrtalama += ogr.Ortalama;
                if (ogr.HarfNotu == "FF")
                    kalanSayisi++;
                else
                    gecenSayisi++;
            }

            double sinifOrtalamasi = toplamOrtalama / dersOgrencileri.Count;

            İstatistik.Items.Clear();
            İstatistik.Items.Add($"Sınıf Mevcudu: {dersOgrencileri.Count}");
            İstatistik.Items.Add($"Sınıf Ortalaması: {sinifOrtalamasi:F2}");
            İstatistik.Items.Add($"Geçen Öğrenci Sayısı: {gecenSayisi}");
            İstatistik.Items.Add($"Kalan Öğrenci Sayısı: {kalanSayisi}");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            txtVize.Text = string.Empty;
            txtFinal.Text = string.Empty;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvOgrenciler.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz öğrencinin bulunduğu satıra tıklayın!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning); // 
                return;
            }

           
            string secilenNumara = dgvOgrenciler.CurrentRow.Cells["Numara"].Value.ToString();
            string secilenAdSoyad = dgvOgrenciler.CurrentRow.Cells["AdSoyad"].Value.ToString();

            
            DialogResult onay = MessageBox.Show($"{secilenAdSoyad} isimli öğrenciyi silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // 

            if (onay == DialogResult.Yes)
            {
                
                Ogrenci silinecekOgr = Program.OgrenciListesi.Find(x => x.Numara == secilenNumara && x.DersAdi == Program.SecilenDers);

                if (silinecekOgr != null)
                {
                   
                    Program.OgrenciListesi.Remove(silinecekOgr); 

                    
                    Program.VerileriKaydet(); 

                    MessageBox.Show("Öğrenci kaydı başarıyla silindi ve dosya güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // 

                   
                    FiltreleVeListele();
                    IstatistikHesapla();
                }
            }
        }
    }
 }

