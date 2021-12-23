using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vize.Models
{
    public class listeleme
    {
      
            public int Id { get; set; }
            public string Marka { get; set; }
            public string Model { get; set; }
            public string ParcaNo { get; set; }
            public DateTime? ÜretimTarihi { get; set; }
            public string ÜretimYeri { get; set; }
            public string SoketTipi { get; set; }
            public string SoketNo { get; set; }
            public string Fiyat { get; set; }
            public int Idd { get; set; }
            public string YazilimAdi { get; set; }
            public string YazilimTuru { get; set; }
            public string Lisans{ get; set; }
            public DateTime? CikisTarihi { get; set; }
            public string UyumluİsletimSistemleri { get; set; }
            public string Fiyatt { get; set; }

    }
    

}