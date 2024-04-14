using MvcSiteMapProvider;
using sklep_internetowy.DAL;
using sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sklep_internetowy.Infrastructure
{
    public class KategorieDynamicNodeProvider :DynamicNodeProviderBase
    {

        private KursyContext db = new KursyContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();
            foreach (Kategoria kat in db.Kategorie)
            {
                DynamicNode node = new DynamicNode();
                node.Title = kat.NazwaKategorii;
                node.Key = "Kategoria_" + kat.KategoriaId;            
                node.RouteValues.Add("nazwaKategorii", kat.NazwaKategorii);
                returnValue.Add(node);
            }

            return returnValue;
        }
    }
}