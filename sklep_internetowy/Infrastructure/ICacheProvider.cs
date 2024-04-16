using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sklep_internetowy.Infrastructure
{
    interface ICacheProvider
    {
        object Get(string key);
        void Set(string key, object data, int caschTime);
        bool IsSet(string key);
        void InValidate(string key);
    }
}
