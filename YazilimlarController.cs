using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vize.Models;


namespace vize.Controllers
{
    public class YazilimlarController : Controller
    {
        public ActionResult Index()
        {
            if (Session["kullanici"] == null)
            {
                Response.Redirect("/LoginForm/SignIn");
            }
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.\"Yazılımlar\" order by \"Id\"", conn);

            var reader = cmd.ExecuteReader();
            List<yazilim> liste = new List<yazilim>();

            while (reader.Read())
            {
                yazilim yzlm = new yazilim();
                yzlm.Id = (int)reader["Id"];
                yzlm.YazilimAdi = reader["YazilimAdi"].ToString();
                yzlm.YazilimTuru = reader["YazilimTuru"].ToString();
                yzlm.Lisans = reader["Lisans"].ToString();
                if (reader["CikisTarihi"].GetType() != typeof(DBNull))
                    yzlm.CikisTarihi = (DateTime)reader["CikisTarihi"];
                yzlm.UyumluİsletimSistemleri = reader["UyumluİsletimSistemleri"].ToString();
                yzlm.Fiyat = reader["Fiyat"].ToString();

                liste.Add(yzlm);
            }
            conn.Close();
            return View(liste);
        }
        public ActionResult Delete(int id)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("Delete from public.\"Yazılımlar\" where \"Id\"= " + id.ToString(), conn);

            cmd.ExecuteNonQuery();

            Response.Redirect("/Yazilimlar/Index");
            return Json("Silindi", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Update(int id)
        {
            MalzemeViewDto dto = GetYazilimObjects();

            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.\"Yazılımlar\" where \"Id\" = " + id, conn);

            var reader = cmd.ExecuteReader();

            yazilim yzlm = new yazilim();
            List<yazilim> list = new List<yazilim>();

            while (reader.Read())
            {

                yzlm.Id = (int)reader["Id"];
                yzlm.YazilimAdi = reader["YazilimAdi"].ToString();
                yzlm.YazilimTuru = reader["YazilimTuru"].ToString();
                yzlm.Lisans = reader["Lisans"].ToString();
                if (reader["CikisTarihi"].GetType() != typeof(DBNull))
                    yzlm.CikisTarihi = (DateTime)reader["CikisTarihi"];
                yzlm.UyumluİsletimSistemleri = reader["UyumluİsletimSistemleri"].ToString();
                yzlm.Fiyat = reader["Fiyat"].ToString();

                list.Add(yzlm);
            }
            conn.Close();
            dto.Yazilim = yzlm;

            return View(dto);
           


        }

        public ActionResult Create()
        {
            MalzemeViewDto dto = GetYazilimObjects();

            return View(dto);
        }
        private static MalzemeViewDto GetYazilimObjects()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            //Yazarları çek
            string sqlyzlm = "select * from public.\"Yazılımlar\"";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlyzlm, conn);
            var reader = cmd.ExecuteReader();
            List<yazilim> list = new List<yazilim>();
            yazilim yzlm = new yazilim();
            while (reader.Read())
            {

                yzlm.Id = (int)reader["Id"];
                yzlm.YazilimAdi = reader["YazilimAdi"].ToString();
                yzlm.YazilimTuru = reader["YazilimTuru"].ToString();
                yzlm.Lisans = reader["Lisans"].ToString();
                if (reader["CikisTarihi"].GetType() != typeof(DBNull))
                    yzlm.CikisTarihi = (DateTime)reader["CikisTarihi"];
                yzlm.UyumluİsletimSistemleri = reader["UyumluİsletimSistemleri"].ToString();
                yzlm.Fiyat = reader["Fiyat"].ToString();

                list.Add(yzlm);
            }
            conn.Close();
            //ve bunları gönder

            MalzemeViewDto dto = new MalzemeViewDto();
            dto.YazilimListe = list;

            return dto;
        }
        public ActionResult YazilimCreate(yazilim Models)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            string sqlString = "INSERT INTO public.\"Yazılımlar\" (\"Id\", \"YazilimAdi\", \"YazilimTuru\", \"Lisans\", \"CikisTarihi\", \"UyumluİsletimSistemleri\",\"Fiyat\") "
             + "values(NEXTVAL('yazilimlar_serial'),'" + Models.YazilimAdi + "', '" + Models.YazilimTuru + "', '" + Models.Lisans + "','" + Models.CikisTarihi+ "', '" + Models.UyumluİsletimSistemleri + "','" + Models.Fiyat + "'  )";
           /* string sqlString = "INSERT INTO public.\"Yazılımlar\" (\"YazilimAdi\", \"YazilimTuru\", \"Lisans\", \"CikisTarihi\", \"UyumluİsletimSistemleri\",\"Fiyat\") "
             + "values'" + Models.YazilimAdi + "', '" + Models.YazilimTuru + "', '" + Models.Lisans + "','" + Models.CikisTarihi + "', '" + Models.UyumluİsletimSistemleri + "','" + Models.Fiyat + "'  )";*/
            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.ExecuteNonQuery();

            Response.Redirect("/Yazilimlar/Index");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YazılımUpdate(yazilim model)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=509;Database=postgres;");

            conn.Open();

            string sqlUpdate = "UPDATE public.\"Yazılımlar\"\n" +
"	SET  \"YazilimAdi\"='" + model.YazilimAdi+"', \"YazilimTuru\"='"+ model.YazilimTuru+"', \"Lisans\"='"+ model.Lisans+ "', \"CikisTarihi\"='" + model.CikisTarihi.Value.ToShortDateString() + 
"', \"UyumluİsletimSistemleri\"='" + model.UyumluİsletimSistemleri + "',\"Fiyat\"='" + model.Fiyat + "'" +"	WHERE \"Id\"= " + model.Id;

            NpgsqlCommand cmd = new NpgsqlCommand(sqlUpdate, conn);

            cmd.ExecuteNonQuery();

            Response.Redirect("/Yazilimlar/Index");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}