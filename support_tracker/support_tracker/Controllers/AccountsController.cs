using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using support_tracker.Auth;
using support_tracker.Models;
using support_tracker.ViewModels;

namespace support_tracker.Controllers
{
    public class AccountsController : Controller
    {
        private StaffManager StaffManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<StaffManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                StaffMember staffMember = new StaffMember { UserName = model.UserName, Email = model.Email };
                IdentityResult result = await StaffManager.CreateAsync(staffMember, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await StaffManager.FindAsync(model.Email, model.Password);
                var foundUserByEmail = await StaffManager.FindByEmailAsync(model.Email);
                bool isUserExist = false;
                if (foundUserByEmail != null)
                {
                    isUserExist = await StaffManager.CheckPasswordAsync(foundUserByEmail, model.Password);
                }
                if (user != null)
                {
                    return await Authenticate(user);
                }
                else if (isUserExist)
                {
                    return await Authenticate(foundUserByEmail);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        private async Task<ActionResult> Authenticate(StaffMember staffMember)
        {
            ClaimsIdentity claim = await StaffManager.CreateIdentityAsync(staffMember,
                   DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
            return Redirect("tickets/get");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}
