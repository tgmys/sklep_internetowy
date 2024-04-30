using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sklep_internetowy.ViewModels
{
    public class KoszykUsuwanieViewModel
    {
        public decimal KoszykCenaCalkowita { set; get; }
        public int KoszykIloscPozycji { set; get; }
        public int IloscPozycjiUsuwanej { set; get; }
        public int IdPozycjiUsuwanej { set; get; }
    }
}