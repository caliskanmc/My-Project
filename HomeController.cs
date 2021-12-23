using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vize.Models;

namespace vize.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["kullanici"] == null)
            {
                Response.Redirect("/LoginForm/SignIn");
            }

            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.\"Malzemeler\" order by \"Id\"", conn);

            var reader = cmd.ExecuteReader();
            List<listeleme> liste = new List<listeleme>();

            while (reader.Read())
            {
                listeleme mlzm = new listeleme();
                mlzm.Id = (int)reader["Id"];
                mlzm.Marka = reader["Marka"].ToString();
                mlzm.Model = reader["Model"].ToString();
                mlzm.ParcaNo = reader["ParcaNo"].ToString();
                mlzm.SoketNo = reader["SoketNo"].ToString();
                mlzm.SoketTipi = reader["SoketTipi"].ToString();
                if (reader["ÜretimTarihi"].GetType() != typeof(DBNull))
                    mlzm.ÜretimTarihi = (DateTime)reader["ÜretimTarihi"];
                mlzm.ÜretimYeri = reader["ÜretimYeri"].ToString();
                mlzm.Fiyat = reader["Fiyat"].ToString();

                liste.Add(mlzm);
            }
            conn.Close();
            

            NpgsqlConnection conn1 = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn1.Open();
            NpgsqlCommand cmd1 = new NpgsqlCommand("Select * from public.\"Yazılımlar\" order by \"Id\"", conn1);

            var reader1 = cmd1.ExecuteReader();
            

            while (reader.Read())
            {
                listeleme yzlm = new listeleme();
                yzlm.Idd = (int)reader1["Id"];
                yzlm.YazilimAdi = reader1["YazilimAdi"].ToString();
                yzlm.YazilimTuru = reader1["YazilimTuru"].ToString();
                yzlm.Lisans = reader1["Lisans"].ToString();
                if (reader1["CikisTarihi"].GetType() != typeof(DBNull))
                    yzlm.CikisTarihi = (DateTime)reader1["CikisTarihi"];
                yzlm.UyumluİsletimSistemleri = reader1["UyumluİsletimSistemleri"].ToString();
                yzlm.Fiyatt = reader1["Fiyat"].ToString();

                liste.Add(yzlm);
            }
            conn1.Close();
            return View(liste);
            
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}