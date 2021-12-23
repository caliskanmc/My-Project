using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vize.Models;

namespace vize.Controllers
{
    public class MalzemelerController : Controller
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
            List<malzeme> liste = new List<malzeme>();

            while (reader.Read())
            {
                malzeme mlzm = new malzeme();
                mlzm.Id = (int)reader["Id"];
                mlzm.Marka = reader["Marka"].ToString();
                mlzm.Modeli = reader["Model"].ToString();
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
            return View(liste);
        }
        public ActionResult Delete(int id)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("Delete from public.\"Malzemeler\" where \"Id\"= " + id.ToString(), conn);

            cmd.ExecuteNonQuery();

            Response.Redirect("/Malzemeler/Index");
            return Json("Silindi", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Update(int id)
        {
            MalzemeViewDto dto = GetMalzemeObjects();

            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.\"Malzemeler\" where \"Id\" = " + id, conn);

            var reader = cmd.ExecuteReader();

            malzeme mlzm = new malzeme();
            List<malzeme> list = new List<malzeme>();

            while (reader.Read())
            {
                
                mlzm.Id = (int)reader["Id"];
                mlzm.Marka = reader["Marka"].ToString();
                mlzm.Modeli = reader["Model"].ToString();
                mlzm.ParcaNo = reader["ParcaNo"].ToString();
                mlzm.SoketNo = reader["SoketNo"].ToString();
                mlzm.SoketTipi = reader["SoketTipi"].ToString();
                if (reader["ÜretimTarihi"].GetType() != typeof(DBNull))
                    mlzm.ÜretimTarihi = (DateTime)reader["ÜretimTarihi"];
                mlzm.ÜretimYeri = reader["ÜretimYeri"].ToString();
                mlzm.Fiyat = reader["Fiyat"].ToString();

                list.Add(mlzm);
            }
            conn.Close();
            dto.Malzeme = mlzm;

            return View(dto);
            /*NpgsqlConnection con = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            string sqlupdate = "Select * from public.\"Malzemeler\" order by \"Id\" = " + id;
            NpgsqlCommand cmdd = new NpgsqlCommand(sqlupdate, conn);
            var readerMalzm = cmdd.ExecuteReader();

            malzeme mlzme = new malzeme();
            if (readerMalzm.Read())
            {                
                mlzme.Id = (int)readerMalzm["Id"];
                mlzme.Marka = readerMalzm["Marka"].ToString();
                mlzme.Model = readerMalzm["Model"].ToString();
                mlzme.ParcaNo = readerMalzm["ParcaNo"].ToString();
                mlzme.SoketNo = readerMalzm["SoketNo"].ToString();
                mlzme.SoketTipi = readerMalzm["SoketTipi"].ToString();
                if (readerMalzm["ÜretimTarihi"].GetType() != typeof(DBNull))
                    mlzme.ÜretimTarihi = (DateTime)readerMalzm["ÜretimTarihi"];
                mlzme.ÜretimYeri = readerMalzm["ÜretimYeri"].ToString();
                mlzme.Fiyat = readerMalzm["Fiyat"].ToString();

            }
            con.Close();*/
            //return View(list);
            

        }

        public ActionResult Create()
        {
            MalzemeViewDto dto = GetMalzemeObjects();

            return View(dto);
        }
        private static MalzemeViewDto GetMalzemeObjects()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            //Yazarları çek
            string sqlmlz = "select * from public.\"Malzemeler\"";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlmlz, conn);
            var reader = cmd.ExecuteReader();
            List<malzeme> list = new List<malzeme>();
            malzeme mlzm = new malzeme();
            while (reader.Read())
            {
                
                mlzm.Id = (int)reader["Id"];
                mlzm.Marka = reader["Marka"].ToString();
                mlzm.Modeli = reader["Model"].ToString();
                mlzm.ParcaNo = reader["ParcaNo"].ToString();
                mlzm.SoketNo = reader["SoketNo"].ToString();
                mlzm.SoketTipi = reader["SoketTipi"].ToString();
                if (reader["ÜretimTarihi"].GetType() != typeof(DBNull))
                    mlzm.ÜretimTarihi = (DateTime)reader["ÜretimTarihi"];
                mlzm.ÜretimYeri = reader["ÜretimYeri"].ToString();
                mlzm.Fiyat = reader["Fiyat"].ToString();
                

                list.Add(mlzm);
            }
            conn.Close();
            //ve bunları gönder

            MalzemeViewDto dto = new MalzemeViewDto();
            dto.MalzemeListe = list;
            
            return dto;
        }
        public ActionResult MalzemeCreate(malzeme Models)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            string sqlString= "INSERT INTO public.\"Malzemeler\" (\"Id\", \"Marka\", \"Model\", \"ParcaNo\", \"ÜretimTarihi\", \"ÜretimYeri\", \"SoketTipi\", \"SoketNo\", \"Fiyat\") "
                    + "values(NEXTVAL('malzeme_serial'),'" + Models.Marka + "', '" + Models.Modeli + "', '" + Models.ParcaNo + "','" + Models.ÜretimTarihi+ "', '" + Models.ÜretimYeri + "', '" + Models.SoketTipi + "', '" + Models.SoketNo + "','" + Models.Fiyat + "')";
            /*string sqlString = "INSERT INTO public.\"Malzemeler\"(\"Marka\", \"Model\", \"ParcaNo\", \"ÜretimTarihi\", \"ÜretimYeri\", \"SoketTipi\", \"SoketNo\", \"Fiyat\")VALUES ('" + Models.Marka + "','" + Models.Modeli + "','" + Models.ParcaNo + "','" + Models.ÜretimTarihi + "','" + Models.ÜretimYeri + "','" + Models.SoketTipi + "','" + Models.SoketNo + "','" + Models.Fiyat + "')";*/
            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.ExecuteNonQuery();

            Response.Redirect("/Malzemeler/Index");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MalzemeUpdate(malzeme model)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=509;Database=postgres;");

            conn.Open();

            string sqlUpdate = "UPDATE public.\"Malzemeler\"\n" +
"	SET  \"Marka\"='" + model.Marka + "', \"Model\"='" + model.Modeli + "', \"ParcaNo\"=" + model.ParcaNo + ", \"ÜretimTarihi\"='" + model.ÜretimTarihi.Value.ToShortDateString() +
"', \"ÜretimYeri\"='" + model.ÜretimYeri+ "', \"SoketTipi\"=" + model.SoketTipi + ", \"SoketNo\"='" + model.SoketNo + "', \"Fiyat\"='" + model.Fiyat + "'" +
"	WHERE \"Id\"= " + model.Id;

            NpgsqlCommand cmd = new NpgsqlCommand(sqlUpdate, conn);

            cmd.ExecuteNonQuery();

            Response.Redirect("/Malzemeler/Index");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
    