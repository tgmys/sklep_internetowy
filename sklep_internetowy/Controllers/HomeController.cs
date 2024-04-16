using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sklep_internetowy.DAL;
using sklep_internetowy.Infrastructure;
using sklep_internetowy.Models;
using sklep_internetowy.ViewModels;

namespace sklep_internetowy.Controllers
{
    public class HomeController : Controller
    {
        private KursyContext db = new KursyContext();
        public ActionResult Index()
        {
            

            ICacheProvider cache = new DefaultCacheProvider();
            List<Kurs> nowosci;
            if (cache.IsSet(Consts.NowosciCacheKey))
            {
                nowosci = cache.Get(Consts.NowosciCacheKey) as List<Kurs>;
            }
            else
            {
                nowosci = db.Kursy.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();
                cache.Set(Consts.NowosciCacheKey, nowosci, 3600);
            }

            List<Kategoria> kategorie;
            if (cache.IsSet(Consts.KategorieCacheKey))
            {
                kategorie = cache.Get(Consts.KategorieCacheKey) as List<Kategoria>;
            }
            else
            {
                kategorie = db.Kategorie.ToList();
                cache.Set(Consts.KategorieCacheKey, kategorie, 3600);
            }

                List<Kurs> bestseller;
            if (cache.IsSet(Consts.BestsellerCacheKey))
            {
                bestseller = cache.Get(Consts.BestsellerCacheKey) as List<Kurs>;
            }
            else
            {
                bestseller = db.Kursy.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Consts.BestsellerCacheKey, bestseller, 3600);
            }

          
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