using Registration.Models;
using Registration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace Registration.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register

        [HttpGet]
        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(PlayerModel model) {
            Security registration = new Security();
            registration.addNewUser(model);
            return View("RegisterPassed");
        }
    }
}