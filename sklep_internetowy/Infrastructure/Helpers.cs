using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
namespace sklep_internetowy.Infrastructure
{
    public class Helpers
    {
        public static void CallUrl(string url)
        {
            var req = HttpWebRequest.Create(url);
            req.GetResponseAsync();
        }

    }
}