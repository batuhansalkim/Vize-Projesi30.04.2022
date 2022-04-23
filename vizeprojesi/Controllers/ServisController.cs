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
        #region soru
        DB03Entities db = new DB03Entities();
        SonucModel sonuc = new SonucModel();

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
        [Route("api/dersialanlar")]

        public List<DersiAlanOgrModel> DersiAlanListe()
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
        [Route("api/dersialanogr/{ogrId}")]

        public DersiAlanOgrModel DersiAlanOgrById(string ogrId)
        {
            DersiAlanOgrModel kayit = db.DersiAlanOgr.Where(s=>s.ogrId==ogrId).Select(x => new DersiAlanOgrModel()
            {

                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdSoyad = x.ogrAdSoyad,
                ogrDogTarih = x.ogrDogTarih,
                ogrFoto = x.ogrFoto,
                UyeId = "1",

            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/dersialanogrekle")]
        public SonucModel DersiAlanOgrEkle(DersiAlanOgrModel model)
        {
            if(db.DersiAlanOgr.Count(s=> s.ogrNo==model.ogrNo)>0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğrenci Numarası Kayıtlıdır";
            }
            return sonuc;
        }


        #endregion
    }
}
