using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vizeprojesi.Models;
using vizeprojesi.ViewModel;

namespace vizeprojesi.Controllers
{
    public class ServisController : ApiController
    {
        
        DB03Entities db = new DB03Entities();
        SonucModel sonuc = new SonucModel();

        #region soru
        [HttpGet]
        [Route("api/soruliste")]

        public List<SoruModel> SoruListe()
        {
            List<SoruModel> soru = db.Soru.Select(x => new SoruModel()
            {
                SoruId = x.SoruId,
                soru1 = x.soru1,
                oA=x.oA,
                oB=x.oB,
                oC=x.oC,
                oD=x.oD,
                ans=x.ans,
                UyeId=x.UyeId
            }).ToList();

            return soru;
        }

        [HttpGet]
        [Route("api/sorubyid/{SoruId}")]

        public SoruModel SoruById(string SoruId)
        {
            SoruModel soru = db.Soru.Where(s => s.SoruId == SoruId).Select(x => new
            SoruModel()
            {
                SoruId = x.SoruId,
                soru1 = x.soru1,
                oA = x.oA,
                oB = x.oB,
                oC = x.oC,
                oD = x.oD,
                ans = x.ans,
                UyeId = x.UyeId

            }).SingleOrDefault();
            return soru;
        }

        [HttpPost]
        [Route("api/soruekle")]

        public SonucModel SoruEkle(SoruModel model)
        {
            if(db.Soru.Count(s=>s.SoruId == model.SoruId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Soru Numarası Kayıtlıdır!";
                return sonuc;
            }
            Soru yeni = new Soru();
            yeni.SoruId = model.SoruId;
            yeni.soru1 = model.soru1;
            yeni.oA = model.oA;
            yeni.oB = model.oB;
            yeni.oC = model.oC;
            yeni.oD = model.oD;
            yeni.ans = model.ans;
            yeni.UyeId = "1";
            db.Soru.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Soru Eklendi";
            return sonuc;
        }


        [HttpPut]
        [Route("api/soruduzenle")]
        public SonucModel SoruDuzenle(SoruModel model)
        {
            Soru kayit = db.Soru.Where(s => s.SoruId == model.SoruId).FirstOrDefault();

            if(kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Bulunamadı";
                return sonuc;
            }

            kayit.soru1 = model.soru1;
            kayit.oA = model.oA;
            kayit.oB = model.oB;
            kayit.oC = model.oC;
            kayit.oD = model.oD;
            kayit.ans = model.ans;
            kayit.UyeId = "1";
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Soru Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/sorusil/{SoruId}")]

        public SonucModel SoruSil(string soruId)
        {
            Soru kayit = db.Soru.Where(s => s.SoruId == soruId).SingleOrDefault();
            {
                if(kayit == null)
                {
                    sonuc.islem = false;
                    sonuc.mesaj = "Soru Bulunamadı";
                    return sonuc;
                }
                db.Soru.Remove(kayit);
                db.SaveChanges();
                sonuc.islem = true;
                sonuc.mesaj = "Soru Silindi";
                return sonuc;
            }
        }
        #endregion

        #region DersiAlanOgrenciler

        [HttpGet]
        [Route("api/dersialanlarliste")]

        public List<DersiAlanOgrModel> DersiAlanOgr()
        {
            List<DersiAlanOgrModel> liste = db.DersiAlanOgr.Select(x => new DersiAlanOgrModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdSoyad = x.ogrAdSoyad,
                ogrDogTarih = x.ogrDogTarih,
                ogrFoto = x.ogrFoto,
                UyeId = "1",
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/dersialanogrbyid/{ogrId}")]

        public DersiAlanOgrModel OgrenciById(string ogrId)
        {
            DersiAlanOgrModel Kayit = db.DersiAlanOgr.Where(s => s.ogrId == ogrId).Select(x =>
             new DersiAlanOgrModel()
             {
                 ogrId = x.ogrId,
                 ogrNo = x.ogrNo,
                 ogrAdSoyad = x.ogrAdSoyad,
                 ogrDogTarih = x.ogrDogTarih,
                 ogrFoto = x.ogrFoto,
                 UyeId = "1",
             }).SingleOrDefault();
            return Kayit;
        }

        [HttpPost]
        [Route("api/ogrenciekle")]

        public SonucModel OgrenciEkle(DersiAlanOgrModel model)
        {
            if (db.DersiAlanOgr.Count(s=> s.ogrNo == model.ogrNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğrenci Numarası Kayıtlıdır";
            }

            DersiAlanOgr yeni = new DersiAlanOgr();
            yeni.ogrId = model.ogrId;
            yeni.ogrNo = model.ogrNo;
            yeni.ogrAdSoyad = model.ogrAdSoyad;
            yeni.ogrDogTarih = model.ogrDogTarih;
            yeni.ogrFoto = model.ogrFoto;
            yeni.UyeId = "1";
            db.DersiAlanOgr.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/ogrenciduzenle")]

        public SonucModel OgrenciDuzenle(DersiAlanOgrModel model)
        {
            DersiAlanOgr kayit = db.DersiAlanOgr.Where(s => s.ogrId == model.ogrId).SingleOrDefault();
            if(kayit==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.ogrNo = model.ogrNo;
            kayit.ogrAdSoyad = model.ogrAdSoyad;
            kayit.ogrDogTarih = model.ogrDogTarih;
            kayit.ogrFoto = model.ogrFoto;
            kayit.UyeId = "1";

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Kaydı Düzenlendi";
            return sonuc;    
        }
        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]

        public SonucModel OgrenciSil(string ogrId)
        {
            DersiAlanOgr kayit = db.DersiAlanOgr.Where(s => s.ogrId == ogrId).SingleOrDefault();
            if(kayit==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc; 
            }

            db.DersiAlanOgr.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi";
            return sonuc;
        }


        #endregion

        #region Dersler 

        [HttpGet]
        [Route("api/dersliste")]

        public List<DerslerModel> DersListe()
        {
            List<DerslerModel> liste = db.Dersler.Select(x => new DerslerModel()
            {
                dersId=x.dersId,
                dersKodu=x.dersKodu,
                dersAdi=x.dersAdi,
                dersKredi=x.dersKredi
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/dersnyid/{dersId}")]

        public DerslerModel DersById(string dersId)
        {
            DerslerModel kayit = db.Dersler.Where(s => s.dersId == dersId).Select(x => new DerslerModel()
            {
                dersId=x.dersId,
                dersKodu=x.dersKodu,
                dersAdi=x.dersAdi,
                dersKredi=x.dersKredi
            }).FirstOrDefault();
            return kayit;
        }
        
        [HttpPost]
        [Route("api/dersekle")]

        public SonucModel DersEkle(Dersler model)
        {
            if(db.Dersler.Count(s=> s.dersKodu == model.dersKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ders Kodu Kayıtlıdır!!";
                return sonuc;
            }
            Dersler yeni = new Dersler();
            yeni.dersId = model.dersId;
            yeni.dersKodu = model.dersKodu;
            yeni.dersAdi = model.dersAdi;
            yeni.dersKredi= model.dersKredi;
            db.Dersler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/dersduzenle")]

        public SonucModel DersDuzenle(DerslerModel model)
        {
            Dersler kayit = db.Dersler.Where(s => s.dersId == model.dersId).FirstOrDefault();

            if(kayit==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunamadı";
                return sonuc;
            }
            kayit.dersKodu = model.dersKodu;
            kayit.dersAdi = model.dersAdi;
            kayit.dersKredi = model.dersAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/derssil/{dersId}")]

        public SonucModel DersSil(string dersId)
        {
            Dersler kayit = db.Dersler.Where(s => s.dersId == dersId).FirstOrDefault();

            if(kayit==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunamadı";
                return sonuc;
            }
            db.Dersler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Silindi";
            return sonuc;
        }

        #endregion
    }
}
