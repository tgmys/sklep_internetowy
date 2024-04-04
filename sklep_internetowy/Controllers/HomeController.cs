using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sklep_internetowy.DAL;
using sklep_internetowy.Models;

namespace sklep_internetowy.Controllers
{
    public class HomeController : Controller
    {
        private KursyContext db = new KursyContext();
        public ActionResult Index()
        {
            var listaKategorii = db.Kategorie.ToList();
  
          
            return View();
        }

        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);

        }
    }
}