using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ETicaretMVC.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ETicaretMVC.DAL
{
    public class SatisContext :DbContext
    {
        public SatisContext () : base("ETicaretVeritabani") { }

        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}