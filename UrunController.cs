using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaretMVC.Models;
using ETicaretMVC.DAL;
using System.Data;

namespace ETicaretMVC.Controllers
{
    public class UrunController : Controller
    {
        SatisContext veritabani = new SatisContext();

        public ActionResult Anasayfa()
        {
            List<Urun> urunler = veritabani.Urunler.ToList();
            return View(urunler);
        }
        public ActionResult Detay(int id)
        {
            Urun urun = (from u in veritabani.Urunler where u.Id == id select u).FirstOrDefault();
            return View(urun);
        }
        public ActionResult Kategori(int id)
        {
            string kategoriAdi = (from k in veritabani.Kategoriler where k.Id == id select k.Ad).FirstOrDefault();
            ViewBag.Title = kategoriAdi + " Kategorisindeki Ürünler";
            List<Urun> urunler = (from u in veritabani.Urunler where u.KategoriId == id select u).ToList();

            return View(urunler);
        }
        public ActionResult SepeteEkle(int id)
        {
            if (Session["KullaniciAdi"] == null)
            {
                return RedirectToAction("KullaniciGirisYap", "Uye");
            }
            Urun urun = (from u in veritabani.Urunler where u.Id == id select u).FirstOrDefault();
            DataTable spt = new DataTable();

            if (Session["sepet"] != null)
            {
                spt = (DataTable)Session["sepet"];
            }
            else
            {
                spt.Columns.Add("Adı");
                spt.Columns.Add("Fiyat");
            }

            DataRow dr = spt.NewRow();
            dr["Adı"] = urun.Ad;
            dr["Fiyat"] = urun.Fiyat;
            spt.Rows.Add(dr);

            Session["sepet"] = spt;
            ViewBag.Toplam = SepetToplam();

            return View(spt);
        }
        public double SepetToplam()
        {
            double toplam = 0;
            if (Session["sepet"] != null)
            {

                DataTable dt = new DataTable();
                dt = (DataTable)Session["sepet"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    toplam += Convert.ToDouble(dt.Rows[i]["Fiyat"].ToString());
                }

            }
            return toplam;
        }
        public ActionResult AdresSecimi()
        {
            string kullanıcımail = Session["KullaniciAdi"].ToString();
            Kullanici kullanıcı = (from a in veritabani.Kullanicilar where a.Email == kullanıcımail select a).FirstOrDefault();
            if (kullanıcı.AdresIs != null)
            {
                ViewBag.AdresIs = "Var";
            }
            return View(kullanıcı);
        }
        public ActionResult SiparisOnay(int Id)
        {
            string kullanicimail = Session["KullaniciAdi"].ToString();
            Kullanici kullanıcı = (from a in veritabani.Kullanicilar where a.Email == kullanicimail  select a).FirstOrDefault();
            if (Id == 1)
            {
                ViewBag.Adres = kullanıcı.AdresEv;
            }
            else
            {
                ViewBag.Adres = kullanıcı.AdresIs;
            }
            ViewBag.Toplam = SepetToplam();
            DataTable dt = new DataTable();
            dt = (DataTable)Session["sepet"];
            return View(dt);
        }
        public ActionResult SiparisiTamamla(string Adres)
        {
            string kullanıcımail = Session["KullaniciAdi"].ToString();
            Kullanici kullanıcı = (from u in veritabani.Kullanicilar where u.Email == kullanıcımail select u).FirstOrDefault();
            ViewBag.Mesaj = "Sayın " + kullanıcı.Ad + " " + kullanıcı.Soyad + " " + "Siparişiniz Onaylanmıştır.";
            DataTable dt = new DataTable();
            dt = (DataTable)Session["sepet"];
            string urunAdı;
            double urunFiyat;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                urunAdı = dt.Rows[i]["Adı"].ToString();
                urunFiyat = double.Parse(dt.Rows[i]["Fiyat"].ToString());

                Siparis siparislerim = new Siparis();
                siparislerim.Ad = urunAdı;
                siparislerim.Fiyat = urunFiyat;
                siparislerim.KullaniciEmail = kullanıcı.Email;
                siparislerim.Adres = Adres;
                veritabani.Siparisler.Add(siparislerim);
                veritabani.SaveChanges();
            }
            Session["sepet"] = null;
            return View();
        }
        public ActionResult Siparislerim()
        {
            string Kullanıcımail = Session["KullaniciAdi"].ToString();
            double ToplamTutar = 0;
            List<Siparis> siparisler = (from u in veritabani.Siparisler where u.KullaniciEmail == Kullanıcımail select u).ToList();
            if (siparisler == null)
            {
                ViewBag.Mesaj = "Siparişiniz Bulunmamaktadır.";
            }
            else
            {
                foreach (var siparis in siparisler)
                {
                    ToplamTutar += siparis.Fiyat;
                }

                ViewBag.Toplam = ToplamTutar;
            }
            return View(siparisler);
        }
    }
}