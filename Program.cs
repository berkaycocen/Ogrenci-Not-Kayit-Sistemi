using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    internal static class Program
    {
        public static List<Ogrenci> OgrenciListesi = new List<Ogrenci>();
        public static string SecilenDers = "";

        private static string dosyaYolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ogrenciler.txt");

        public static void VerileriKaydet()
        {
            try
            {

                using (StreamWriter sw = new StreamWriter(dosyaYolu, false))
                {
                    foreach (var ogr in OgrenciListesi)
                    {

                        string satir = $"{ogr.Numara}|{ogr.AdSoyad}|{ogr.DersAdi}|{ogr.VizeNotu}|{ogr.FinalNotu}|{ogr.Ortalama}|{ogr.HarfNotu}";
                        sw.WriteLine(satir);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler dosyaya kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void VerileriOku()
        {
            try
            {
                
                if (!File.Exists(dosyaYolu)) return;

                OgrenciListesi.Clear(); 

               
                string[] satirlar = File.ReadAllLines(dosyaYolu);

                foreach (string satir in satirlar)
                {
                    if (string.IsNullOrWhiteSpace(satir)) continue;

                    
                    string[] parcalar = satir.Split('|');

                    if (parcalar.Length == 7)
                    {
                        Ogrenci ogr = new Ogrenci
                        {
                            Numara = parcalar[0],
                            AdSoyad = parcalar[1],
                            DersAdi = parcalar[2],
                            VizeNotu = Convert.ToInt32(parcalar[3]),
                            FinalNotu = Convert.ToInt32(parcalar[4]),
                            Ortalama = Convert.ToDouble(parcalar[5]),
                            HarfNotu = parcalar[6]
                        };
                        OgrenciListesi.Add(ogr);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler dosyadan okunurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            VerileriOku();
            Application.Run(new Form1());
        }
    }
}
