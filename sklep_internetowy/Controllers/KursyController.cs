using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sklep_internetowy.Controllers
{
    public class KursyController : Controller
    {
        // GET: Kursy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Lista(string nazwaKategorii)
        {
            return View();
        }
        
        public ActionResult Szczegoly( string id)
        {
            return View();
        }
    }
}