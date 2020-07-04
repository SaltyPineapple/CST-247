using Activity1Part3.Models;
using Activity1Part3.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Activity1Part3.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(UserModel model) {
            SecurityService securityCheck = new SecurityService();
            var secure = securityCheck.Authenticate(model);

            if (secure) {
                return View("LoginPassed");
            }
            else {
                return View("LoginFailed", model);
            }

        }
    }
}