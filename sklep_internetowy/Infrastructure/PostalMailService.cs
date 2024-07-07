using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sklep_internetowy.Models;
using sklep_internetowy.ViewModels;
namespace sklep_internetowy.Infrastructure
{
    public class PostalMailService : IMailService
    {
        public void WyslaniePotwierdzenieZamowieniaEmail(Zamowienie zamowienie)
        {
            PotwierdzenieZamowieniaEmail email = new PotwierdzenieZamowieniaEmail();
            email.To = zamowienie.Email;
            email.From = "skleponline@gmail.com";
            email.Wartosc = zamowienie.WartoscZamowienia;
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.PozycjeZamowienia = zamowienie.PozycjeZamowienia;
            Console.WriteLine(zamowienie.PozycjeZamowienia);
            email.Send();
        }

        public void WyslanieZamowienieZrealizowaneEmail(Zamowienie zamowienie)
        {
            ZamowienieZrealizowaneEmail email = new ZamowienieZrealizowaneEmail();
            email.To = zamowienie.Email;
            email.From = "skleponline@gmail.com";
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.Send();

        }
    }
}