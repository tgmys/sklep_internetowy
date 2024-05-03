using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sklep_internetowy.Models
{
    public class Zamowienie
    {
        public int ZamowienieID { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Wprowadz imię")]
        [StringLength(50)]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwisko")]
        [StringLength(50)]
        public string Nazwisko { get; set; }
        [Required(ErrorMessage = "Wprowadz ulicę")]
        [StringLength(100)]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Wprowadz miasto")]
        [StringLength(100)]
        public string Miasto { get; set; }
        [Required(ErrorMessage = "Wprowadz kod pocztowy")]
        [StringLength(6)]
        public string KodPocztowy { get; set; }
        [Required(ErrorMessage ="Musisz wprowadzić numer telefonu")]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string Telefon { get; set; }
        [Required(ErrorMessage ="Wprowadz swoj adres e-mail")]
        [EmailAddress(ErrorMessage ="Błędny format adresu e-mail")]
        public string Email { get; set; }
        public string Komentarz { get; set; }
        public DateTime DataDodania { get; set; }
        public StanZamowienia StenZamowienia { get; set; }
        public decimal WartoscZamowienia { get; set; }

        public List<PozycjaZamowienia> PozycjeZamowienia { get; set; }
    }

    public enum StanZamowienia
    {
        Nowe,
        Zrealizowane
    }
}