using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace sklep_internetowy.Infrastructure
{
    public class AppConfig
    {
        private static string _ikonyKategoriForderWzgledny= ConfigurationManager.AppSettings["IkonyKategoriiFolder"];

            public static string IkonyKategoriFolderWzgledny
        {
            get
            {
                return _ikonyKategoriForderWzgledny;
            }
        }

        private static string _obrazkiForderWzgledny = ConfigurationManager.AppSettings["ObrazkiFolder"];

        public static string ObrazkiFolderWzgledny
        {
            get
            {
                return _obrazkiForderWzgledny;
            }
        }
    }
}