using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechEvent
{
    public static class TechEventHelper
    {
        public static List<TechEventKullanici> kullaniciListesi = new List<TechEventKullanici>();
        public static List<Etkinlik> etkinlikListesi = new List<Etkinlik>();
        public static List<BiletFirmalari> biletFirmalari = new List<BiletFirmalari>()
        {
            new Biletci(){ FirmaAdi="Biletci", WebSitesi="www.biletci.com" },
            new BiletEvi(){ FirmaAdi="Bilet Evi", WebSitesi="www.biletevi.com" },
            new TicketAgent(){ FirmaAdi="Ticket Agent", WebSitesi="www.ticketagent.com" }
        };

        static string BUYUK_HARFLER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static string kucuk_harfler = "abcdefghijklmnopqrstuvwxyz";
        public static bool BuyukHarfliSifre(string sifre)
        {
            int sayac = 0;
            foreach (var item in sifre)
            {
                if (BUYUK_HARFLER.Contains(item))
                {
                    sayac++;
                }
            }

            if (sayac >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool KucukHarfliSifre(string sifre)
        {
            int sayac = 0;
            foreach (var item in sifre)
            {
                if (kucuk_harfler.Contains(item))
                {
                    sayac++;
                }
            }

            if (sayac >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string XMLDataOlustur()
        {
            string xml = "<Etkinlikler>";

            foreach (Etkinlik item in etkinlikListesi)
            {
                xml += @$"<Etkinlik>
                            <Ad>{item.EtkinlikAdi}</Ad>
                            <Sehir>{item.EtkinliginOlduguSehir}</Sehir>
                            <Tarih>{item.EtkinlikTarihi.ToShortDateString()}</Tarih>
                            <KisiSayisi>{item.KatilacakKisiSayisi}</KisiSayisi>
                            <Aciklama>{item.Aciklama}</Aciklama>
                          </Etkinlik>";
            }

            xml += "</Etkinlikler>";

            return xml;
        }

        public static string JSONDataGonder()
        {
            string json = "[";

            foreach (Etkinlik item in etkinlikListesi)
            {
                json += "{'Name' : " + item.EtkinlikAdi + ", 'City' : " + item.EtkinliginOlduguSehir + ", 'Date' : " + item.EtkinlikTarihi.ToString("yyyy.MM.dd") + ", 'ParticipantCount' : " + item.KatilacakKisiSayisi + ", 'Description' : " + item.Aciklama + " },";
            }

            json += "]";

            return json;
        }
    }
}
