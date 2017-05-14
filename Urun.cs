using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ETicaretMVC.Models
{
    public class Urun
    {
        public int Id { get; set; }
        public int KategoriId { get; set; }

        [DisplayName("Ürün Adı")]
        public string Ad { get; set; }

        [DisplayName("Fiyat")]
        public double Fiyat { get; set; }

        public virtual Kategori Kategori { get; set; }
    }
}