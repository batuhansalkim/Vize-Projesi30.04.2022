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

            }).FirstOrDefault();
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


        #endregion
    }
}
