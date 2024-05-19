using Microsoft.AspNet.Identity.Owin;
using sklep_internetowy.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using sklep_internetowy.Models;
using sklep_internetowy.Infrastructure;
using sklep_internetowy.ViewModels;


namespace sklep_internetowy.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        public enum MenageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        private ApplicationUserManager _userManager;

        public ManageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ManageController()
        {

        }

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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: Manage
        public async Task<ActionResult> Index(MenageMessageId? message)
        {
            if(TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if(user ==null)
            {
                return View("Error");
            }
            var model = new ManageCredentialsViewModel
            {
                Message=message,
                DaneUzytkownika= user.DaneUzytkownika

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DaneUzytkownika")] DaneUzytkownika daneUzytkownika)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.DaneUzytkownika = daneUzytkownika;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = MenageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = MenageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

    }
}