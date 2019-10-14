using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using support_tracker.Auth;
using support_tracker.Models;
using support_tracker.ViewModels;

namespace support_tracker.Controllers
{
    public class AccountsController : Controller
    {
        private readonly StaffManager staffManager;
        private readonly IAuthenticationManager authenticationManager;
        public AccountsController(AuthHelper authHelper)
        {
            this.staffManager = authHelper.GetStaffManagerFromOwinContext;
            this.authenticationManager = authHelper.GetAuthenticationManagerFromOwinContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                StaffMember staffMember = new StaffMember { UserName = model.UserName, Email = model.Email };
                IdentityResult result = await staffManager.CreateAsync(staffMember, model.Password);
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await staffManager.FindAsync(model.Email, model.Password);
                var foundUserByEmail = await staffManager.FindByEmailAsync(model.Email);
                bool isUserExist = false;
                if (foundUserByEmail != null)
                {
                    isUserExist = await staffManager.CheckPasswordAsync(foundUserByEmail, model.Password);
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
                    ModelState.AddModelError("", Constants_files.Constants.INVALIDA_LOGIN_DATA);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        private async Task<ActionResult> Authenticate(StaffMember staffMember)
        {
            ClaimsIdentity claim = await staffManager.CreateIdentityAsync(staffMember,
                   DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut();
            authenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
            return RedirectToAction("GetTickets", "Tickets");
        }

        public ActionResult Logout()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}
