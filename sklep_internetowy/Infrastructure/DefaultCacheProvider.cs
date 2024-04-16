using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Web;

namespace sklep_internetowy.Infrastructure
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private Cache cache {get{ return HttpContext.Current.Cache; } }
        public object Get(string key)
        {
            return cache[key];
        }

        public void InValidate(string key)
        {
            cache.Remove(key);
        }

        public bool IsSet(string key)
        {
            return (cache[key] != null);
        }

        public void Set(string key, object data, int caschTime)
        {
            var expirationTime = DateTime.Now + TimeSpan.FromMinutes(caschTime);
            cache.Insert(key, data, null,expirationTime,Cache.NoSlidingExpiration);
        }
    }
}