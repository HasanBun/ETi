using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETicaretMVC.Models;
using System.Data.Entity;

namespace ETicaretMVC.DAL
{
    public class SatisInitializer : CreateDatabaseIfNotExists<SatisContext>
    {
        protected override void Seed(SatisContext context)
        {
            var kategoriBilgisayar = new Kategori { Ad = "Bilgisayar" };
            var kategoriCepTelefonu = new Kategori { Ad = "Cep Telefonu" };
            var kategoriTelevizyon = new Kategori { Ad = "Televizyon" };
            var kategoriBeyazEsya = new Kategori { Ad = "Beyaz Esya" };
            context.Kategoriler.Add(kategoriBilgisayar);
            context.Kategoriler.Add(kategoriCepTelefonu);
            context.Kategoriler.Add(kategoriTelevizyon);
            context.Kategoriler.Add(kategoriBeyazEsya);
            context.SaveChanges();
            List<Urun> urunler = new List<Urun>
            {
                new Urun { Ad = "Hp Notebook", Fiyat = 2000, KategoriId = kategoriBilgisayar.Id },
                new Urun { Ad = "Samsung Tablet", Fiyat = 5000, KategoriId = kategoriBilgisayar.Id },
                new Urun { Ad = "Iphone 7", Fiyat = 3900, KategoriId = kategoriCepTelefonu.Id },
                new Urun { Ad = "Samsung S8", Fiyat = 3900, KategoriId = kategoriCepTelefonu.Id },
                new Urun { Ad = "LG Led Tv", Fiyat = 2500, KategoriId = kategoriTelevizyon.Id },
                new Urun { Ad = "Vestel Smart Tv", Fiyat = 3000, KategoriId = kategoriTelevizyon.Id },
                new Urun { Ad = "Arçelik Buzdolabı", Fiyat = 3000, KategoriId = kategoriBeyazEsya.Id }

            };
            urunler.ForEach(urun => context.Urunler.Add(urun));
            context.SaveChanges();
        }
    }
}