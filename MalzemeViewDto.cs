using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace vize.Models
{
    public class MalzemeViewDto
    {
        public List<malzeme> MalzemeListe { get; set; }

        public malzeme Malzeme { get; set; }
        public List<yazilim> YazilimListe { get; set; }

        public yazilim Yazilim { get; set; }
        public List<Login> LoginListe { get; set; }
        
    }
}
