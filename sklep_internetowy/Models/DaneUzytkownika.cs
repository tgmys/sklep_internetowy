﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace sklep_internetowy.Models
{
    public class DaneUzytkownika
    {
        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Adres { get; set; }

        public string Miasto { get; set; }
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string Telefon { get; set; }
        [EmailAddress(ErrorMessage ="Błędny format adresu e-mail")]
        public string Email { get; set; }
    }
}