using Blog.BLL;
using Blog.BLL.ServiceInterfaces;
using Blog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Blog.UI.Controllers
{
    [Authorize(Roles ="admin, user, corrector, blocked ")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _accountService.RegistrationUser(viewModel);
                return RedirectToAction("Login");
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               _accountService.UserLogin(model);
                return RedirectToAction("RegistredIndex", "Blog");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _accountService.UserOut();
            return RedirectToAction("RegistredIndex", "Blog");
        }
    }
}