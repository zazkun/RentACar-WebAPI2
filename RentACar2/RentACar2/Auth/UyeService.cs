using RentACar2.Models;
using RentACar2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar2.Auth
{
    public class UyeService
    {
        DB01Entities db = new DB01Entities();

        public UyeModel UyeOturumAc(string kAdi, string parola)
        {
            UyeModel uye = db.Uye.Where(s => s.kullaniciAdi == kAdi && s.sifre == parola).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                adsoyad = x.adsoyad,
                email = x.email,
                kullaniciAdi = x.kullaniciAdi,
                uyeFotograf = x.uyeFotograf,
                sifre = x.sifre,
                uyeAdmin = x.uyeAdmin

            }).SingleOrDefault();

        }

        internal object UyeOturumAc(Func<object, bool> equals, string password)
        {
            throw new NotImplementedException();
        }
    }
}