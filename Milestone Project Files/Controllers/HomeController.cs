using Registration.Services.Utility;
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
            //Logging
            MyLogger.GetInstance().Info(" Entering Home Controller");

            return View();
        }

        /*
         * This click listener redirects user to appropriate page
         */
        [HttpPost]
        public ActionResult OnButtonClick(String homeButtonValue)
        {
            try
            {
                //Logging
                MyLogger.GetInstance().Info(" Inside OnButtonClick method");

                if (homeButtonValue == "login")
                {
                    return View("~/Views/Login/Index.cshtml");
                }
                else if (homeButtonValue == "register")
                {
                    return View("~/Views/Register/Register.cshtml");
                }
                else
                {
                    return View("Index");
                }
            }
            catch(Exception e)
            {
                MyLogger.GetInstance().Info(" Exception inside OnButtonClick -->" + e.Message);
                return View("Index"); 
            }            
        }
    }
}