using Activity1Part3.Models;
using Activity1Part3.Services.Business;
using Activity1Part3.Services.Utility;
using NLog;
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

            // put an item in the log
            MyLogger.GetInstance().Info("Entering the login controller. Login Method");
            
            
            try {
                //Validate the Form POST
                if (!ModelState.IsValid) {
                    return View("Login");
                }

                SecurityService securityCheck = new SecurityService();
                var secure = securityCheck.Authenticate(model);

                if (secure) {
                    MyLogger.GetInstance().Info("Exiting login controller. Login success!");
                    return View("LoginPassed");
                }
                else {
                    MyLogger.GetInstance().Info("Exiting login controller. Login Failed");
                    return View("LoginFailed", model);
                }
            }
            catch (Exception e){
                MyLogger.GetInstance().Error("Exception! " + e.Message);
                return Content("Exception in login" + e.Message);
            }

        }
    }
}