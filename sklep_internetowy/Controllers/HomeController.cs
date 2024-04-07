using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sklep_internetowy.DAL;
using sklep_internetowy.Models;
using sklep_internetowy.ViewModels;

namespace sklep_internetowy.Controllers
{
    public class HomeController : Controller
    {
        private KursyContext db = new KursyContext();
        public ActionResult Index()
        {
            var kategorie = db.Kategorie.ToList();
            var nowosci = db.Kursy.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();
            var bestseller = db.Kursy.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
            var vm = new HomeViewModel()
            {
                Kategorie = kategorie,
                Nowosci = nowosci,
                Betsellery = bestseller
            };
            return View(vm);
        }

        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);

        }
    }
}