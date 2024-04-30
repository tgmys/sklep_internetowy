using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sklep_internetowy.Models;

namespace sklep_internetowy.ViewModels
{
    public class KoszykViewModel
    {
        public List<PozycjaKoszyka> PozycjeKoszyka { set; get; }

        public decimal CenaCalkowita { set; get; }


    }
}