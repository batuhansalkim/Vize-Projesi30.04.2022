using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vizeprojesi.ViewModel
{
    public class DersiAlanOgrModel
    {
        public string ogrId { get; set; }
        public string ogrNo { get; set; }
        public string ogrAdSoyad { get; set; }
        public string ogrDogTarih { get; set; }
        public byte[] ogrFoto { get; set; }
        public string UyeId { get; set; }
    }
}