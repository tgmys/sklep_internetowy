﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace sklep_internetowy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "KursyLista",
            url: "Kategoria/{nazwaKategorii}",
            defaults: new { controller = "Kursy", action = "Lista" }
            );

            routes.MapRoute(
           name: "KursySzczegoly",
           url: "kurs-{id}.html",
           defaults: new { controller = "Kursy", action = "Szczegoly" }
           );

            routes.MapRoute(
                name: "StronyStatyczne",
                url: "strona/{nazwa}.html",
                defaults: new { controller = "Home", action = "StronyStatyczne" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
