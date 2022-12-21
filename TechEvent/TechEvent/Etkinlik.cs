using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechEvent
{
    public class Etkinlik
    {
        public Etkinlik(string etkinlikAdi, DateTime etkinlikTarihi, string etkinliginOlduguSehir, string aciklama, int katilacakKisiSayisi, bool biletliMi)
        {
            EtkinlikAdi = etkinlikAdi;
            EtkinlikTarihi = etkinlikTarihi;
            EtkinliginOlduguSehir = etkinliginOlduguSehir;
            Aciklama = aciklama;
            KatilacakKisiSayisi = katilacakKisiSayisi;
            BiletliMi = biletliMi;
            KatilacakKisiler = new List<TechEventKullanici>();
        }

        public string EtkinlikAdi { get; set; }
        public DateTime EtkinlikTarihi { get; set; }
        public string EtkinliginOlduguSehir { get; set; }
        public string Aciklama { get; set; }
        public int KatilacakKisiSayisi { get; set; }
        public bool BiletliMi { get; set; }
        public List<TechEventKullanici> KatilacakKisiler { get; set; }

        
    }
}
