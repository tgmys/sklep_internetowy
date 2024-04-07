using sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sklep_internetowy.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public IEnumerable<Kurs> Nowosci { get; set; }
        public IEnumerable<Kurs> Betsellery { get; set; }
    }
}