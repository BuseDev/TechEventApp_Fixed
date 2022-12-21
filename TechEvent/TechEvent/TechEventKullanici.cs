using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TechEvent
{
    public class TechEventKullanici
    {
        public string ad;
        public string soyad;
        public string kAdi;
        public string sifre;
        public int tip; // 1 olursa organizatör, 2 olursa bilet satın alacak kullanıcı

        public bool Kaydol(string ad, string soyad, string kAdi, string sifre, int tip)
        {
            foreach (TechEventKullanici item in TechEventHelper.kullaniciListesi)
            {
                if (item.kAdi == kAdi)
                {
                    throw new Exception("Bu kullanıcı adı zaten kayıtlı");
                }
            }

            if (!(tip == 1 || tip == 2))
            {
                throw new Exception("Organizatör için 1'e, diğer için 2'ye basılması gerekmektedir.");
            }

            return true;
        }

        public bool CheckName(string kadi, List<TechEventKullanici> kullanicilar)
        {
            foreach (TechEventKullanici item in kullanicilar)
            {
                if (item.kAdi != kadi)
                {
                    Console.WriteLine("Kullanıcı adı yanlış! Tekrar deneyin...");
                    return true;
                }
            }
            this.kAdi = kadi;
            return false;
        }

        public bool CheckPassword(string sifre, List<TechEventKullanici> kullanicilar)
        {
            foreach (TechEventKullanici item in kullanicilar)
            {
                if (item.sifre != sifre)
                {
                    Console.WriteLine("Şifre yanlış! Tekrar deneyin...");
                    return true;
                }
            }

            return false;
        }

        public bool Giris(string kadi, string sifre, List<TechEventKullanici> kullanicilar)
        {
            foreach (TechEventKullanici item in kullanicilar)
            {
                if (item.kAdi == kadi && item.sifre == sifre)
                {
                    return true;
                }
            }

            return false;
        }

        public void HesapKapatma(string kullanici, List<TechEventKullanici> kullaniciListesi)
        {
            // Hata veriyor, düzeltilecek
            foreach (TechEventKullanici item in kullaniciListesi.ToList())
            {
                if (item.kAdi == kullanici)
                {
                    kullaniciListesi.Remove(item);
                    if (kullaniciListesi.Count == 0)
                    {
                        Console.WriteLine("Hesap kapandı");
                        Thread.Sleep(4000);
                    }
                    Environment.Exit(0);
                }
            }
        }

        public void OrganizatorKullanicisiIcinEtkinlikOlustur(string etkinlikAdi, DateTime etkinlikTarihi, string etkinliginOlduguSehir, string aciklama, int katilacakKisiSayisi, bool biletliMi, int tip)
        {
            if (tip == 1)
            {
                try
                {
                    Etkinlik etkinlik = new Etkinlik(etkinlikAdi, etkinlikTarihi, etkinliginOlduguSehir, aciklama, katilacakKisiSayisi, biletliMi);
                    TechEventHelper.etkinlikListesi.Add(etkinlik);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine(tip);
                throw new Exception("Sadece organizatör rolündeki kullanıcılar etkinlik oluşturabilir");
            }
        }

        public void BirEtkinligeKatilma(Etkinlik etkinlik)
        {
            if (this.tip == 2)
            {
                if (etkinlik.KatilacakKisiler.Count < etkinlik.KatilacakKisiSayisi)
                {
                    etkinlik.KatilacakKisiler.Add(this);
                }
                else
                {
                    throw new Exception("Etkinliğin kontenjanı dolmuştur");
                }
            }
            else
            {
                throw new Exception("Sadece katilimci rolündeki kullanıcılar bir etkinliğe katılabilir");
            }
        }

        public void BiletAl()
        {
            foreach (BiletFirmalari item in TechEventHelper.biletFirmalari)
            {
                if (!item.DataCekildiMi)
                {
                    throw new Exception($"{item.FirmaAdi} firmasında biletler henüz satışa çıkmamıştır");
                }
            }

            Console.WriteLine("Aşağıdaki adreslerden bilet alabilirsiniz");
            TechEventHelper.biletFirmalari.ForEach(firma =>
            {
                Console.WriteLine($"{firma.FirmaAdi} -> {firma.WebSitesi}");
            });
        }
    }
}
