using sklep_internetowy.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sklep_internetowy.Controllers
{
    public class KursyController : Controller
    {
        private KursyContext db = new KursyContext();
        // GET: Kursy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Lista(string nazwaKategorii)
        {
            var kategoria = db.Kategorie.Include("Kursy").Where(k => k.NazwaKategorii.ToUpper() == nazwaKategorii.ToUpper()).Single();
            var kursy = kategoria.Kursy.ToList();
            return View(kursy);
        }
        
        public ActionResult Szczegoly( int id)
        {
            var kurs = db.Kursy.Find(id);

            return View(kurs);
        }
        
        [ChildActionOnly]
        public ActionResult KategorieMenu(string id)
        {
            var Kategorie = db.Kategorie.ToList();

            return PartialView("_KategorieMenu", Kategorie);
        }
    }
}