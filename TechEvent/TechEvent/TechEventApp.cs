using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TechEvent
{
    internal class TechEventApp
    {
        public int type;
        public string lastUserName;
        public void Start()
        {
            Console.WriteLine("TechEvent uygulamasına hoş geldiniz.");
            Console.WriteLine();
            Console.WriteLine("1. Kayıt Ol");
            Console.WriteLine("2. Giriş Yap");

            int x = int.Parse(Console.ReadLine());
            TechEventKullanici user = new TechEventKullanici();
            if (x == 1)
            {
                bool isSuccess;
                do
                {
                    isSuccess = AddUser();
                } while (!isSuccess);

                LogIn(user);

                LoadOperations(user);
            }
            if (x == 2)
            {
                LogIn(user);

                LoadOperations(user);
            }
        }

        TechEventKullanici LogIn(TechEventKullanici result)
        {
            do
            {
                result = LoginUser();
            } while (result == null);
            return result;
        }
        // a - result tutmalı

        void LoadOperations(TechEventKullanici user)
        {
            try
            {
                int y;
                Console.WriteLine();
                if (type == 1)
                    Console.WriteLine("1.Etkinlik Oluştur");

                if (type == 2)
                {
                    Console.WriteLine("2.Etkinliğe Katıl");
                    Console.WriteLine("3.Bilet Al");
                }

                Console.WriteLine("4.Hesabını Kapat");

                y = int.Parse(Console.ReadLine());

                switch (y)
                {
                    case 1:
                        Create(user);
                        break;
                    case 2:
                        Join(user);
                        break;
                    case 3:
                        Sale(user);
                        break;
                    case 4:
                        user.HesapKapatma(lastUserName, TechEventHelper.kullaniciListesi);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Sale(TechEventKullanici user)
        {
            try
            {
                user.BiletAl();
            }
            catch (Exception ex)
            {
                foreach (BiletFirmalari item in TechEventHelper.biletFirmalari)
                {
                    item.EtkinlikleriXMLOlarakAlir();
                    item.EtkinlikleriJSONOlarakAlir();
                }

                user.BiletAl();
                Console.WriteLine(ex.Message);
            }
        }

        void Join(TechEventKullanici user)
        {
            Console.WriteLine("Katılmak istediğiniz etkinliği seçin");
            foreach (Etkinlik item in TechEventHelper.etkinlikListesi)
            {
                Console.WriteLine("1. " + item.EtkinlikAdi);
            }

            int z = int.Parse(Console.ReadLine());
            Etkinlik @event = TechEventHelper.etkinlikListesi[z - 1];

            try
            {
                user.BirEtkinligeKatilma(@event);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Create(TechEventKullanici user)
        {
            Console.Write("Etkinlik adı : ");
            string name = Console.ReadLine();
            Console.Write("Açıklama : ");
            string description = Console.ReadLine();

            DateTime date = new DateTime();
            do
            {
                Console.Write("Tarih : ");
                date = DateTime.Parse(Console.ReadLine());

            } while (EtkinlikTarihiKontrolEt(date));

            int count = 0;
            do
            {
                Console.Write("Kişi Sayısı : ");
                count = int.Parse(Console.ReadLine());
            } while (KisiSayisiniKontrolEt(count));

            string city = "";
            do
            {
                Console.Write("Hangi şehir : ");
                city = Console.ReadLine();
            } while (EtkinlikSehriniKontrolEt(city));

            Console.Write("Biletli mi (E\\H) : ");
            char answer = char.Parse(Console.ReadLine());
            BiletliMi(answer);

            try
            {
                user.OrganizatorKullanicisiIcinEtkinlikOlustur(name, date, city, description, count, (answer == 'E'), this.type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void BiletliMi(char answer)
        {
            if (answer == 'e' || answer == 'E')
            {
                Random rnd = new Random();
                Console.WriteLine();
                Console.WriteLine("Biletiniz : " + rnd.Next(100000000, 1000000000) + rnd.Next(1000000, 10000000));
                Console.WriteLine();
                Console.WriteLine("İşleminiz başarıyla sonlandı...");
                Thread.Sleep(4000);
                Environment.Exit(0);
            }
            if (answer == 'h' || answer == 'H')
            {
                Console.WriteLine();
                Console.WriteLine("İşleminiz başarıyla sonlandı...");
                Thread.Sleep(4000);
                Environment.Exit(0);
            }
        }
        bool EtkinlikTarihiKontrolEt(DateTime dateTime)
        {
            if (dateTime < DateTime.Now.AddMonths(1))
            {
                Console.WriteLine("Etkinlik tarihi en erken 1 ay sonra olmalıdır");
                return true;
            }
            else
                return false;
        }

        bool EtkinlikSehriniKontrolEt(string sehir)
        {
            string[] sehirler = { "istanbul", "ankara", "izmir" };
            if (!sehirler.Contains(sehir.ToLower()))
            {
                Console.WriteLine("Şu an için İstanbul,Ankara ve İzmir'de etkinlik düzenlenebilir");
                return true;
            }
            else
                return false;
        }

        bool KisiSayisiniKontrolEt(int sayi)
        {
            if (sayi < 15)
            {
                Console.WriteLine("En az 15 kişilik etkinlik oluşturulabilir");
                return true;
            }
            else
                return false;
        }

        TechEventKullanici LoginUser()
        {
            TechEventKullanici appUser = new TechEventKullanici();
            string user = "";
            do
            {
                Console.Write("Kullanıcı adı : ");
                user = Console.ReadLine();
            } while (appUser.CheckName(user, TechEventHelper.kullaniciListesi));
            lastUserName = user;
            string password = "";
            do
            {
                Console.Write("Şifre : ");
                password = Console.ReadLine();
            } while (appUser.CheckPassword(password, TechEventHelper.kullaniciListesi));


            bool result = appUser.Giris(user, password, TechEventHelper.kullaniciListesi);
            if (result)
            {
                Console.WriteLine("Giriş başarılı");
                appUser = TechEventHelper.kullaniciListesi.SingleOrDefault(a => a.kAdi == user && a.sifre == password);
                return appUser;
            }
            else
            {
                Console.WriteLine("Giriş başarısız");
                return null;
            }
        }

        bool AddUser()
        {
            Console.Write("Ad : ");
            string name = Console.ReadLine();
            Console.Write("Soyad : ");
            string surname = Console.ReadLine();
            Console.Write("Kullanıcı adı : ");
            string user = Console.ReadLine();
            string password = "";
            do
            {
                Console.Write("Şifre : ");
                password = Console.ReadLine();
            }
            while (check(password));

            Console.WriteLine("Organizatör için 1'e, kullanıcı için 2'ye basın");
            int type = int.Parse(Console.ReadLine());
            this.type = type;

            try
            {
                TechEventKullanici appUser = new TechEventKullanici();
                bool result = appUser.Kaydol(name, surname, user, password, type);
                if (result)
                {
                    TechEventHelper.kullaniciListesi.Add(new TechEventKullanici() { ad = name, soyad = surname, kAdi = user, sifre = password, tip = type });
                    return true;
                }
                else
                {
                    Console.WriteLine("Kayıt başarısız");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        bool check(string sifre)
        {
            if (sifre.Length < 6 || TechEventHelper.BuyukHarfliSifre(sifre) == false || TechEventHelper.KucukHarfliSifre(sifre) == false)
            {
                Console.WriteLine("Şifre en az 6 karakterli olmalı ve en az 2 büyük ve küçük harf içermelidir");
                return true;
            }
            else
                return false;

        }
    }
}
