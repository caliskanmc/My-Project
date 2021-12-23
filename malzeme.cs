using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vize.Models
{
    public class malzeme
    {   public int Id { get; set; }
        public string Marka { get; set; }
        public string Modeli { get; set; }
        public string ParcaNo { get; set; }
        public DateTime? ÜretimTarihi { get; set; }
        public string ÜretimYeri { get; set; }
        public string SoketTipi { get; set; }
        public string SoketNo { get; set; }
        public string Fiyat { get; set; }
    }
}