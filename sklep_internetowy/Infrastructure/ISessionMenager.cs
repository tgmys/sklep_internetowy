using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sklep_internetowy.Infrastructure
{
    public interface ISessionMenager
    {
        T Get<T>(string key);
        void Set<T>(string name, T value);
        void Abandon();
        T tryGet<T>(string key);
    }
}
