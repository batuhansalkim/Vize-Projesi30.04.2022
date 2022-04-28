using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vizeprojesi.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitDersiAlanOgrId { get; set; }
        public string kayitDerslerId { get; set; }
        public DersiAlanOgrModel ogrBilgi { get; set; }
        public DerslerModel dersBilgi { get; set; }
    }
}