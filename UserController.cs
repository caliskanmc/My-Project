using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using vize.Models;
using System.ComponentModel.DataAnnotations;
namespace vize.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            
            return View();
        }
        private static MalzemeViewDto GetMalzemeObjects()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Port=5432; User Id=postgres;Password=509;Database=postgres;");

            conn.Open();
            //Yazarları çek
            string sqluser = "select * from public.\"Kullanıcı_kayıt\"";
            NpgsqlCommand cmd = new NpgsqlCommand(sqluser, conn);
            var reader = cmd.ExecuteReader();
            List<Login> list = new List<Login>();
            Login lgn = new Login();
            while (reader.Read())
            {

                lgn.Adı = reader["Adı"].ToString();
                lgn.Soyadı = reader["Soyadı"].ToString();
                lgn.Tckimlik = reader["Tckimlik"].ToString();
                lgn.CepTelefonu = reader["CepTelefonu"].ToString();
                lgn.KullaniciAdi = reader["KullaniciAdi"].ToString();
                if (reader["DogumTarihi"].GetType() != typeof(DBNull))
                    lgn.DogumTarihi = (DateTime)reader["DogumTarihi"];
                lgn.Sifre = reader["Sifre"].ToString();
                lgn.SifreTekrar = reader["SifreTekrar"].ToString();


                list.Add(lgn);
            }
            conn.Close();
            //ve bunları gönder

            MalzemeViewDto dto = new MalzemeViewDto();
            dto.LoginListe = list;

            return dto;
        }
        public ActionResult Create()
        {
            MalzemeViewDto dto = GetMalzemeObjects();

            return View(dto);
        }
        
        public ActionResult YeniKullanici(Login Models)
        {
           
            return View();
        }

        public ActionResult YeniKullaniciKaydet(Login Models)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=509;Database=postgres;");

            conn.Open();

            string sqlString = "INSERT INTO public.\"Kullanıcı_kayıt\"(\"Adı\", \"Soyadı\", \"Tckimlik\", \"DogumTarihi\", \"CepTelefonu\", \"KullaniciAdi\", \"Sifre\", \"SifreTekrar\")VALUES ('" + Models.Adı + "','" + Models.Soyadı + "','" + Models.Tckimlik + "','" + Models.DogumTarihi + "','" + Models.CepTelefonu + "','" + Models.KullaniciAdi + "','" + Models.Sifre + "','" + Models.SifreTekrar + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.ExecuteNonQuery();


            Response.Redirect("/LoginForm/SignIn");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}