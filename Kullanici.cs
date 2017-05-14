using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaretMVC.Models
{
    public class Kullanici
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string Ad { get; set; }

        [Required]
        [DisplayName("Soyadı")]
        public string Soyad { get; set; }

        [Required]
        [DisplayName("E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required]
        [DisplayName("Şifreyi Tekrar Yazınız")]
        [DataType(DataType.Password)]
        [Compare("Sifre")]
        public string Sifre2 { get; set; }

        [Required]
        [DisplayName("Adres")]
        public string AdresEv { get; set; }

        [DisplayName("Adres2")]
        public string AdresIs { get; set; }
    }
}