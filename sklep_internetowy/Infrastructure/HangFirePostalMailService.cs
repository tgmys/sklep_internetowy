using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using sklep_internetowy.Models;
using System.Web.Mvc;

namespace sklep_internetowy.Infrastructure
{
    public class HangFirePostalMailService: IMailService
    {
        public void WyslaniePotwierdzenieZamowieniaEmail(Zamowienie zamowienie, byte[] pdf)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("WyslaniePotwierdzenieZamowieniaEmail", "Manage", new { zamowienieId = zamowienie.ZamowienieID, nazwisko = zamowienie.Nazwisko }, HttpContext.Current.Request.Url.Scheme);
            BackgroundJob.Enqueue(() => Helpers.CallUrl(url));
   
        }

        public void WyslanieZamowienieZrealizowaneEmail(Zamowienie zamowienie)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("WyslanieZamowienieZrealizowaneEmail", "Manage", new { zamowienieId = zamowienie.ZamowienieID, nazwisko = zamowienie.Nazwisko }, HttpContext.Current.Request.Url.Scheme);

            BackgroundJob.Enqueue(() => Helpers.CallUrl(url));
        }
    }
}