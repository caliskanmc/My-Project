using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vize.Models;


namespace vize.Controllers
{
    public class LoginFormController : Controller
    {
        // GET: LoginForm
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult Creat()
        {
            MalzemeViewDto dto = GetMalzemeObjects();

            return View(dto);
        }
        private static MalzemeViewDto GetMalzemeObjects()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            //Yazarları çek
            string sqllgn = "select * from public.\"Kullanıcı_kayıt\"";
            NpgsqlCommand cmd = new NpgsqlCommand(sqllgn, conn);
            var reader = cmd.ExecuteReader();

            Login lgn = new Login();

            while (reader.Read())
            {

                /*lgn.Adı = reader["Adı"].ToString();
                lgn.Soyadı = reader["Soyadı"].ToString();
                lgn.Tckimlik = reader["Tckimlik"].ToString();
                lgn.CepTelefonu = reader["CepTelefonu"].ToString();
                lgn.KullaniciAdi = reader["KullaniciAdi"].ToString();
                if (reader["DogumTarihi"].GetType() != typeof(DBNull))
                    lgn.DogumTarihi = (DateTime)reader["DogumTarihi"];
                lgn.Sifre = reader["Sifre"].ToString();
                lgn.SifreTekrar = reader["SifreTekrar"].ToString();*/



            }
            conn.Close();
            //ve bunları gönder

            MalzemeViewDto dto = new MalzemeViewDto();


            return dto;
        }
        public ActionResult CreateUser(Login Models)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=509;Database=postgres;");

            conn.Open();

            string sqlString = "INSERT INTO public.\"Kullanıcı_kayıt\"(\"Adı\", \"Soyadı\", \"Tckimlik\", \"DogumTarihi\", \"CepTelefonu\", \"KullaniciAdi\", \"Sifre\", \"SifreTekrar\")VALUES ('" + Models.Adı + "','" + Models.Soyadı + "','" + Models.Tckimlik + "','" + Models.DogumTarihi + "','" + Models.CepTelefonu + "','" + Models.KullaniciAdi + "','" + Models.Sifre + "','" + Models.SifreTekrar + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.ExecuteNonQuery();


            Response.Redirect("/LoginForm/SignIn");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Giris(string kullaniciadi, string sifre)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=509;Database=postgres;");

            conn.Open();

            string sqlKullanici = "SELECT * FROM public.\"Kullanıcı_kayıt\" where \"KullaniciAdi\"=@KAdi and \"Sifre\"=@Sifre";

            //tert' or '1' = '1

            NpgsqlCommand cmd = new NpgsqlCommand(sqlKullanici, conn);
            cmd.Parameters.AddWithValue("KAdi", kullaniciadi);
            cmd.Parameters.AddWithValue("Sifre", sifre);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Session["kullanici"] = kullaniciadi;
                Response.Redirect("/Home/Index");
            }
            else
            {
                Session["kullanici"] = null;
            }
            Hata hata = new Hata();
            hata.Mesaj = "Kullanıcı adı veya şifre yanlış.";
            return View("SignIn", hata);
        }

        public ActionResult Signout()
        {
            
            Session["kullanici"] = null;
            Response.Redirect("/LoginForm/SignIn");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }

    public class Hata
    {
        public string Mesaj { get; set; }
    }
}

