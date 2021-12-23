using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vize.Models
{
    public class yazilim
    {
        public int Id { get; set; }
        public string YazilimAdi { get; set; }
        public string YazilimTuru { get; set; }
        public string Lisans { get; set; }
        public DateTime? CikisTarihi { get; set; }
        public string UyumluİsletimSistemleri { get; set; }
        public string Fiyat { get; set; }
    }
}