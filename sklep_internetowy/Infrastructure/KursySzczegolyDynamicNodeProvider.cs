using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using sklep_internetowy.DAL;
using sklep_internetowy.Models;

namespace sklep_internetowy.Infrastructure
{
    public class KursySzczegolyDynamicNodeProvider : DynamicNodeProviderBase
    {
        private KursyContext db = new KursyContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();
            foreach(Kurs kurs in db.Kursy )
            {
                DynamicNode node = new DynamicNode();
                node.Title = kurs.TytulKursu;
                node.Key = "Kurs_" + kurs.KursId;
                node.ParentKey = "Kategoria_" + kurs.KategoriaID;
                node.RouteValues.Add("id", kurs.KursId);
                returnValue.Add(node);
            }

            return returnValue;
        }

    }
}