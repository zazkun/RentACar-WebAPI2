using RentACar2.Models;
using RentACar2.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentACar2.Controllers
{
    [Authorize]
    public class ServisController : ApiController
    {

        DB01Entities db = new DB01Entities();
        SonucModel sonuc = new SonucModel();

        #region Kategori

        [HttpGet]
        [Route("api/kategoriliste")]

        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategoriAdi = x.kategoriAdi
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.kategoriId == katId).Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategoriAdi = x.kategoriAdi,
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.kategoriAdi == model.kategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlı.Lütfen Tekrar Deneyiniz! ";

                return sonuc;
            }

            Kategori yeni = new Kategori(); yeni.kategoriAdi = model.kategoriAdi; db.Kategori.Add(yeni); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == model.kategoriId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }

            kayit.kategoriAdi = model.kategoriAdi; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Yeniden Düzenlendi.";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == katId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadı!";

                return sonuc;
            }

            if (db.Arac.Count(s => s.aracKategoriId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ürün Kayıtlı Olan Kategori Silinemez!";

                return sonuc;
            }

            db.Kategori.Remove(kayit); db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi.";

            return sonuc;
        }
        #endregion

        #region Arac


        [HttpGet]
        [Route("api/aracliste")]
        public List<AracModel> AracListe()
        {
            List<AracModel> liste = db.Arac.Select(x => new AracModel()
            {
                aracId = x.aracId,
                aracModel = x.aracModel,
                aracFoto = x.aracFoto,
                aracFiyat = x.aracFiyat,
                aracKategoriId = x.aracKategoriId,
                aracUyeId = x.aracUyeId,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/araclisteByKatId/{KatId}")]
        public List<AracModel> AracListeByKatId(int KatId)
        {
            List<AracModel> liste = db.Arac.Where(s => s.aracKategoriId == KatId).Select(x => new AracModel()
            {
                aracId = x.aracId,
                aracModel = x.aracModel,
                aracFoto = x.aracFoto,
                aracFiyat = x.aracFiyat,
                aracKategoriId = x.aracKategoriId,
                aracUyeId = x.aracUyeId,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/araclisteByUyeId/{UyeId}")]
        public List<AracModel> AracListeByUyeId(int UyeId)
        {
            List<AracModel> liste = db.Arac.Where(s => s.aracUyeId == UyeId).Select(x => new AracModel()
            {
                aracId = x.aracId,
                aracModel = x.aracModel,
                aracFoto = x.aracFoto,
                aracFiyat = x.aracFiyat,
                aracKategoriId = x.aracKategoriId,
                aracUyeId = x.aracUyeId,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/AracById/{AracId}")]
        public AracModel AracById(int AracId)
        {
            AracModel kayit = db.Arac.Where(s => s.aracId == AracId).Select(x => new AracModel()
            {
                aracId = x.aracId,
                aracModel = x.aracModel,
                aracFoto = x.aracFoto,
                aracFiyat = x.aracFiyat,
                aracKategoriId = x.aracKategoriId,
                aracUyeId = x.aracUyeId,
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/Aracekle")]
        public SonucModel MakaleEkle(AracModel model)
        {
            if (db.Arac.Count(s => s.aracModel == model.aracModel) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Araç Modeli Kayıtlıdır!";

                return sonuc;
            }
            Arac yeni = new Arac();
            yeni.aracModel = model.aracModel;
            yeni.aracFiyat = model.aracFiyat;
            yeni.aracId = model.aracId;
            yeni.aracKategoriId = model.aracKategoriId;
            yeni.aracUyeId = model.aracUyeId;
            yeni.aracFoto = model.aracFoto;
            db.Arac.Add(yeni);
            db.SaveChanges();



            sonuc.islem = true; sonuc.mesaj = "Araç Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/AracDuzenle")]
        public SonucModel MakaleDuzenle(AracModel model)
        {
            Arac kayit = db.Arac.Where(s => s.aracId == model.aracId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıtlı Araç Bulunamadı!";

                return sonuc;
            }
            kayit.aracModel = model.aracModel;
            kayit.aracFiyat = model.aracFiyat;
            kayit.aracId = model.aracId;
            kayit.aracKategoriId = model.aracKategoriId;
            kayit.aracUyeId = model.aracUyeId;
            kayit.aracFoto = model.aracFoto;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Araç Kaydı Düzenlendi";

            return sonuc;

        }

        [HttpDelete]
        [Route("api/AracSil/{AracId}")]
        public SonucModel AracSil(int AracId)
        {
            Arac kayit = db.Arac.Where(s => s.aracId == AracId).SingleOrDefault(
);
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıtlı Araç Bulunamadı!";

                return sonuc;
            }

            if (db.Kiralama.Count(s => s.kiraAracId == AracId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Henüz Kiralanmamış Araçlar Silinemez!";

                return sonuc;
            }
            db.Arac.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Araç Silindi";

            return sonuc;
        }

        [HttpPost]
        [Route("api/aracfotoguncelle")]
        public SonucModel AracFotoGuncelle(AracFotoModel model)
        {
            Arac arac = db.Arac.Where(s => s.aracFoto == model.aracId).SingleOrDefault();

            if (arac == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Araç Bulunamadı!";

                return sonuc;
            }

            if (arac.aracFoto != "arac.jpg")
            {
                string yol = System.Web.Hosting.HostingEnvironment.MapPath("~/Folders/" + arac.aracFoto);

                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
            }

            string data = model.aracFotoData;
            string base64 = data.Substring(data.IndexOf(',') + 1); base64 = base64.Trim('\0');
            byte[] imgbytes = Convert.FromBase64String(base64);
            string dosyaAdi = arac.aracId + model.AracFotoUzanti.Replace("image/", ".");

            using (var ms = new MemoryStream(imgbytes, 0, imgbytes.Length))
            {
                Image img = Image.FromStream(ms, true);
                img.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Folders/" + dosyaAdi));
            }
            arac.aracFoto = dosyaAdi; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Araç Fotoğrafı Güncellendi";

            return sonuc;

        }
        #endregion

        #region Üye 

        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                adsoyad = x.adsoyad,
                email = x.email,
                kullaniciAdi = x.kullaniciAdi,
                uyeFotograf = x.uyeFotograf,
                sifre = x.sifre,
                uyeAdmin = x.uyeAdmin
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/uyeById/{UyeId}")]
        public UyeModel UyeById(int UyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.uyeId == UyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                adsoyad = x.adsoyad,
                email = x.email,
                kullaniciAdi = x.kullaniciAdi,
                uyeFotograf = x.uyeFotograf,
                sifre = x.sifre,
                uyeAdmin = x.uyeAdmin
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/uyeEkle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.kullaniciAdi == model.kullaniciAdi || s.email == model.email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı veya E-Posta Adresi Kayıtlıdır.";
                return sonuc;
            }

            Uye yeni = new Uye();
            yeni.adsoyad = model.adsoyad;
            yeni.email = model.email;
            yeni.kullaniciAdi = model.kullaniciAdi;
            yeni.uyeFotograf = model.uyeFotograf;
            yeni.sifre = model.sifre;
            yeni.uyeAdmin = model.uyeAdmin;
            db.Uye.Add(yeni);

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeDuzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";

                return sonuc;
            }
            kayit.adsoyad = model.adsoyad;
            kayit.email = model.email;
            kayit.kullaniciAdi = model.kullaniciAdi;
            kayit.uyeFotograf = model.uyeFotograf;
            kayit.sifre = model.sifre;
            kayit.uyeAdmin = model.uyeAdmin;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyeSil/{UyeId}")]
        public SonucModel UyeSil(int UyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == UyeId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";

                return sonuc;
            }

            if (db.Arac.Count(s => s.aracUyeId == UyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Araç Kiralan Üyeler Silinemez!";

                return sonuc;
            }

            if (db.Kiralama.Count(s => s.kiraUyeId == UyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Araç Kira Kaydı Bulunan Üye Silinemez!";

                return sonuc;
            }

            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";

            return sonuc;
        }

        [HttpPost]
        [Route("api/uyefotoguncelle")]
        public SonucModel OgrFotoGuncelle(uyeFotoModel model)
        {
            Uye uye = db.Uye.Where(s => s.uyeFotograf == model.uyeId).SingleOrDefault();

            if (uye == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";

                return sonuc;
            }

            if (uye.uyeFotograf != "profil.jpg")
            {
                string yol = System.Web.Hosting.HostingEnvironment.MapPath("~/Folders/"+ uye.uyeFotograf);

                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
            }

            string data = model.fotoData;
            string base64 = data.Substring(data.IndexOf(',') + 1);base64 = base64.Trim('\0');
            byte[] imgbytes = Convert.FromBase64String(base64);
            string dosyaAdi = uye.uyeId + model.dosyaUzanti.Replace("image/", ".");

            using (var ms = new MemoryStream(imgbytes, 0, imgbytes.Length))
            {
                Image img = Image.FromStream(ms, true);
                img.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Folders/" + dosyaAdi));
            }
            uye.uyeFotograf = dosyaAdi; db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Fotoğraf Güncellendi";

            return sonuc;

        }


        #endregion

        #region Kiralama

        [HttpGet]
        [Route("api/kiraById/{KiraId}")]

        public KiralamaModel YorumById(int KiraId)
        {
            KiralamaModel kayit = db.Kiralama.Where(s => s.kiraId == KiraId).Select(x => new KiralamaModel()
            {
                kiraId = x.kiraId,
                aracPlakaNo = x.aracPlakaNo,
                kiraBasGun = x.kiraBasGun,
                kiraSonGun = x.kiraSonGun,
                kiraAracId = x.kiraAracId,
                kiraUyeId = x.kiraUyeId,
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kiralİkEkle")]
        public SonucModel KiralıkEkle(KiralamaModel model)
        {
            if (db.Kiralama.Count(s => s.kiraUyeId == model.kiraUyeId && s.kiraAracId == model.kiraAracId && s.aracPlakaNo == model.aracPlakaNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Kişi, Aynı Araç ve Plakaya Sahip Kiralık Araca Sahip Olamaz!";

                return sonuc;
            }

            Kiralama yeni = new Kiralama();
            yeni.kiraId = model.kiraId;
            yeni.aracPlakaNo = model.aracPlakaNo;
            yeni.kiraBasGun = model.kiraBasGun;
            yeni.kiraSonGun = model.kiraSonGun;
            yeni.kiraUyeId = model.kiraUyeId;
            yeni.kiraAracId = model.kiraAracId;


            db.Kiralama.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Bir Yeni Kiralık Araç Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/kiralikDuzenle")]
        public SonucModel KiralikDuzenle(KiralamaModel model)
        {
            Kiralama kayit = db.Kiralama.Where(s => s.kiraId == model.kiraId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kiralık Araç Kaydı Bulunamadı!";

                return sonuc;
            }

            kayit.kiraId = model.kiraId;
            kayit.aracPlakaNo = model.aracPlakaNo;
            kayit.kiraBasGun = model.kiraBasGun;
            kayit.kiraSonGun = model.kiraSonGun;
            kayit.kiraUyeId = model.kiraUyeId;
            kayit.kiraId = model.kiraId;


            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kiralık Araç Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/KiralikSil/{KiraId}")]
        public SonucModel KiralıkSil(int KiraId)
        {
            Kiralama kayit = db.Kiralama.Where(s => s.kiraId == KiraId).SingleOrDefault();

            if (kayit == null)

            {
                sonuc.islem = false;
                sonuc.mesaj = "Kiralık Araç Kaydı Bulunamadı!";

                return sonuc;

            }
            db.Kiralama.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum Silindi";

            return sonuc;
        }




        [HttpGet]
        [Route("api/kiraliste")]
        public List<KiralamaModel> KiraListe()
        {
            List<KiralamaModel> liste = db.Kiralama.Select(x => new KiralamaModel()
            {
                kiraId = x.kiraId,
                aracPlakaNo = x.aracPlakaNo,
                kiraAracId = x.kiraAracId,
                kiraUyeId = x.kiraUyeId,
                kiraBasGun = x.kiraBasGun,
                kiraSonGun = x.kiraSonGun,
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kiralisteByUyeId/{UyeId}")]
        public List<KiralamaModel> KiraListeByUyeId(int UyeId)
        {
            List<KiralamaModel> liste = db.Kiralama.Where(s => s.kiraUyeId == UyeId).Select(x => new KiralamaModel()
            {
                kiraId = x.kiraId,
                aracPlakaNo = x.aracPlakaNo,
                kiraAracId = x.kiraAracId,
                kiraUyeId = x.kiraUyeId,
                kiraBasGun = x.kiraBasGun,
                kiraSonGun = x.kiraSonGun,
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kiralisteByAracId/{AracId}")]
        public List<KiralamaModel> KiraListeByAracId(int AracId)
        {
            List<KiralamaModel> liste = db.Kiralama.Where(s => s.kiraAracId == AracId).Select(x => new KiralamaModel()
            {
                kiraId = x.kiraId,
                aracPlakaNo = x.aracPlakaNo,
                kiraAracId = x.kiraAracId,
                kiraUyeId = x.kiraUyeId,
                kiraBasGun = x.kiraBasGun,
                kiraSonGun = x.kiraSonGun,
            }).ToList();

            return liste;
        }

        #endregion

    }
}
