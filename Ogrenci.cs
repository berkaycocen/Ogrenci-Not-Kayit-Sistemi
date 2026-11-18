using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje
{
    public class Ogrenci
    {
        public string Numara { get; set; }
        public string AdSoyad { get; set; }

        public string DersAdi { get; set; }
        public int VizeNotu { get; set; }
        public int FinalNotu { get; set; }
        public double Ortalama { get; set; }
        public string HarfNotu { get; set; }


        public void OrtalamaHesapla()
        {
            Ortalama = (VizeNotu * 0.4) + (FinalNotu * 0.6);
            HarfNotuHesapla();
        }

        private void HarfNotuHesapla()
        {
            if (Ortalama >= 90) HarfNotu = "AA";
            else if (Ortalama >= 85) HarfNotu = "BA";
            else if (Ortalama >= 80) HarfNotu = "BB";
            else if (Ortalama >= 75) HarfNotu = "CB";
            else if (Ortalama >= 65) HarfNotu = "CC";
            else if (Ortalama >= 58) HarfNotu = "DC";
            else if (Ortalama >= 50) HarfNotu = "DD";
            else HarfNotu = "FF";
        }
    }
}
