using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sklep_internetowy.Models;
using sklep_internetowy.ViewModels;
using System.IO;
using System.Net.Mail;
namespace sklep_internetowy.Infrastructure
{
    public class PostalMailService : IMailService
    {
        public void WyslaniePotwierdzenieZamowieniaEmail(Zamowienie zamowienie, byte[] pdf = null)
        {
            if (zamowienie.PozycjeZamowienia == null)
            {
                throw new InvalidOperationException("Pozycje zamówienia są niepoprawne.");
            }
            PotwierdzenieZamowieniaEmail email = new PotwierdzenieZamowieniaEmail();
            email.To = zamowienie.Email;
            email.From = "skleponline@gmail.com";
            email.Wartosc = zamowienie.WartoscZamowienia;
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.PozycjeZamowienia = zamowienie.PozycjeZamowienia;
            email.sciezkaObrazka = AppConfig.ObrazkiFolderWzgledny;

            if (pdf != null && pdf.Length > 0)
            {
                email.Attach(new Attachment(new MemoryStream(pdf), "PotwierdzenieZamowienia.pdf"));
            }

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