using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar2.ViewModel
{
    public class AracModel
    {
        public int aracId { get; set; }
        public string aracModel { get; set; }
        public string aracFoto { get; set; }
        public decimal aracFiyat { get; set; }
        public int aracKategoriId { get; set; }
        public int aracUyeId { get; set; }

    }
}