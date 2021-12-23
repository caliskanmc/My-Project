using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vize.Models
{
    public class Login
    {
        public string Adı { get; set; }
        public string Soyadı{ get; set; }
        public string Tckimlik { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string  CepTelefonu{ get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string SifreTekrar { get; set; }
    }
}