using ETicaretMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaretMVC.DAL;

namespace ETicaretMVC.Controllers
{
    public class UyeController : Controller
    {
        SatisContext veritabani = new SatisContext();

        public ActionResult KullaniciEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KullaniciEkle(Kullanici uye)
        {
            if (ModelState.IsValid)
            {
                veritabani.Kullanicilar.Add(uye);
                veritabani.SaveChanges();
                return RedirectToAction("Anasayfa", "Urun");
            }
            else
            {
                ModelState.AddModelError("", "");
            }
            return View();
        }
        public ActionResult KullanıcıBilgisi()
        {
            string KullaniciEmail = Session["KullaniciAdi"].ToString();
            Kullanici kullanıcıbilgi = (from k in veritabani.Kullanicilar where k.Email == KullaniciEmail select k).FirstOrDefault();
            return View(kullanıcıbilgi);
        }
        public ActionResult KullaniciGirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KullaniciGirisYap(Kullanici giris)
        {
            Kullanici admin = (from u in veritabani.Kullanicilar where u.Email == giris.Email && u.Sifre == giris.Sifre select u).FirstOrDefault();
            Session["KullaniciAdi"] = null;
            if (admin != null)
            {
                Session["KullaniciAdi"] = admin.Email;
                return RedirectToAction("Anasayfa", "Urun");
            }
            else
            {
                return RedirectToAction("KullaniciGirisYap");
            }
        }
        public ActionResult Cıkıs()
        {
            Session["KullaniciAdi"] = null;
            Session["sepet"] = null;
            return RedirectToAction("Anasayfa", "Urun");
        }
    }
}