using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sklep_internetowy.Models;
namespace sklep_internetowy.ViewModels
{
    public class FakturaViewModel
    {
        public int ZamowienieID { get; set; }
        public DateTime DataZamowienia { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Miasto { get; set; }
        public string KodPocztowy { get; set; }
        public string Telefon { get; set; }
        public decimal WartoscZamowienia { get; set; }
        public List<PozycjaZamowienia> PozycjeZamowienia { get; set; }
    }
}