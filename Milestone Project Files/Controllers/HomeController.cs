using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registration.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OnButtonClick(String homeButtonValue)
        {
            if(homeButtonValue == "login")
            {
                return View("~/Views/Login/Index.cshtml");
            }
            else if(homeButtonValue == "register")
            {
                return View("~/Views/Register/Register.cshtml");
            }
            else
            {
                return View("Index");
            }
        }
    }
}