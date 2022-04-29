using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar2.ViewModel
{
    public class KiralamaModel
    {
        public int kiraId { get; set; }
        public string aracPlakaNo { get; set; }
        public System.DateTime kiraBasGun { get; set; }
        public System.DateTime kiraSonGun { get; set; }
        public int kiraUyeId { get; set; }
        public int kiraAracId { get; set; }
    }
}