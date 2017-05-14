using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ETicaretMVC.Models
{
    public class Siparis
    {
        public int SiparisId { get; set; }

        [DisplayName("Ürün Adı")]
        public string Ad { get; set; }

        [DisplayName("Fiyat")]
        public double Fiyat { get; set; }

        [DisplayName("Kullanıcı Bilgisi")]
        public string KullaniciEmail { get; set; }

        [DisplayName("Adres")]
        public string Adres { get; set; }
    }
}