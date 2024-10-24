using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sklep_internetowy.Models;
namespace sklep_internetowy.Infrastructure
{
    public interface IMailService
    {
        void WyslaniePotwierdzenieZamowieniaEmail(Zamowienie zamowienie, byte[] pdf);
        void WyslanieZamowienieZrealizowaneEmail(Zamowienie zamowienie);
    }
}
