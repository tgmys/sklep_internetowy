using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using sklep_internetowy.App_Start;
using sklep_internetowy.DAL;
using sklep_internetowy.Infrastructure;
using sklep_internetowy.Models;
using sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Web.Mvc;
using NLog;
using Hangfire;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using SelectPdf;
using Postal;
namespace sklep_internetowy.Controllers
{
    public class KoszykController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private KoszykMenager koszykMenager;
        private ISessionMenager sessionMenager { get; set; }
        private KursyContext db;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IMailService maileService;



        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public KoszykController(KursyContext context, IMailService maileService, ISessionMenager sessionMenager)
        {
            this.db = context;
            this.maileService = maileService;
            this.sessionMenager = sessionMenager;
            koszykMenager = new KoszykMenager(sessionMenager, db);
        }

        public KoszykController(IMailService maileService)
        {
            this.maileService = maileService;
            db = new KursyContext();
            sessionMenager = new SessionMenager();
            koszykMenager = new KoszykMenager(sessionMenager, db);
        }

        // GET: Koszyk
        public ActionResult Index()
        {
            var pozycjeKoszyka = koszykMenager.PobierzKoszyk();
            var cenaCalkowita = koszykMenager.PobierzWartoscKoszyka();
            KoszykViewModel koszykVM = new KoszykViewModel()
            {
                PozycjeKoszyka = pozycjeKoszyka,
                CenaCalkowita =cenaCalkowita
                
            };
            return View(koszykVM);
        }

        public ActionResult DodajDoKoszyka(int id)
        {
            koszykMenager.DodajDoKoszyka(id);
            return RedirectToAction("Index");
        }

        public int PobierzIloscElementowKoszyka()
        {
            return koszykMenager.PobierzIloscPozycjiKoszyka();
        }

        public ActionResult UsunZKoszyka( int kursId)
        {
            int iloscPozycji = koszykMenager.UsunZKoszyka(kursId);
            int iloscPozycjiKoszyka = koszykMenager.PobierzIloscPozycjiKoszyka();
            decimal wartoscKoszyka = koszykMenager.PobierzWartoscKoszyka();

            var wynik = new KoszykUsuwanieViewModel
            {
                IdPozycjiUsuwanej = kursId,
                IloscPozycjiUsuwanej = iloscPozycji,
                KoszykCenaCalkowita = wartoscKoszyka,
                KoszykIloscPozycji = iloscPozycjiKoszyka

            };

            return Json(wynik);
        }

        public async Task<ActionResult> Zaplac()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var zamowienie = new Zamowienie
                {
                    Imie = user.DaneUzytkownika.Imie,
                    Nazwisko = user.DaneUzytkownika.Nazwisko,
                    Adres = user.DaneUzytkownika.Adres,
                    Miasto = user.DaneUzytkownika.Miasto,
                    KodPocztowy=user.DaneUzytkownika.KodPocztowy,
                    Email=user.DaneUzytkownika.Email,
                    Telefon=user.DaneUzytkownika.Telefon

                };

                return View(zamowienie);
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Zaplac", "Koszyk") });
        }

        [HttpPost]
        public async Task<ActionResult> Zaplac(Zamowienie zamowienieSzczegoly)
        {

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var newOrder = koszykMenager.UtworzZamowienie(zamowienieSzczegoly, userId);

                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.DaneUzytkownika);
                await UserManager.UpdateAsync(user);

                koszykMenager.PustyKoszyk();

                string html = RenderViewToString("~/Views/Raporty/Report.cshtml", newOrder);

                // Konwersja HTML na PDF
                HtmlToPdf oHtmlToPdf = new HtmlToPdf();
                PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html);

                // Dodawanie hasła do pliku PDF, jeśli wymagane
                oPdfDocument.Security.UserPassword = "password";

                // Zapisanie PDF do tablicy bajtów
                byte[] pdf = oPdfDocument.Save();
                oPdfDocument.Close();
                // BackgroundJob.Enqueue(() => System.Console.WriteLine("Testowe zadanie w tle"));
                // BackgroundJob.Schedule(() => System.Console.WriteLine("Test zadanie na jutro do wykonania"), TimeSpan.FromDays(1));
                // RecurringJob.AddOrUpdate(() => Console.WriteLine("Zadanie codziennie"), Cron.Daily);

               //  string url = Url.Action("PotwierdzenieZamowieniaEmail", "Koszyk", new { zamowienieId = newOrder.ZamowienieID, nazwisko = newOrder.Nazwisko }, Request.Url.Scheme);
              //    BackgroundJob.Enqueue(() => Call(url));

                maileService.WyslaniePotwierdzenieZamowieniaEmail(newOrder, pdf);
                return RedirectToAction("PotwierdzenieZamowienia");

            }
            else
                return View(zamowienieSzczegoly);
        }
        public static void Call(string url)
        {
            var req = HttpWebRequest.Create(url);
            req.GetResponseAsync();
        }

        public ActionResult PotwierdzenieZamowienia()
        {
            var name = User.Identity.Name;
            logger.Info("Strona koszyk | potwierdzenie | " + name);
            return View();
        }

        public ActionResult GeneratePDF(int zamowienieId, string nazwisko, string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");

            HtmlToPdf oHtmlToPdf = new HtmlToPdf();
            PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html);
            byte[] pdf = oPdfDocument.Save();
            oPdfDocument.Close();

            return File(pdf, "application/pdf", "test.pdf");
        }


        private string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}