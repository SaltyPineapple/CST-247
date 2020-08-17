using Milestone2.Models;
using Milestone2.Services.Business;
using NLog;
using Registration.Services.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

/* Patrick Garcia
 * 
 */

namespace Milestone2.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Index()
        {
            //Logging
            MyLogger.GetInstance().Info(" Entering Login Controller");

            LoginModel login = new LoginModel();
            return View(login);
        }

        /*
         * This method is used to log user into system
         */
        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            //Logging
            MyLogger.GetInstance().Info(" Entering Login Method in Login Controller");
            try
            {
                //Data validation
                if (ModelState.IsValid)
                {
                    SecurityService service = new SecurityService();

                    //Calling helper methods to check credentials
                    bool flag = service.Authenticate(user);

                    if (flag) //Succesful login
                    {
                        //Logging
                        MyLogger.GetInstance().Info(" Successful Login --> Exiting Login Controller");                        
                        //Saving user in session
                        Session["user"] = user;

                        FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                        return View("LoginSuccess", user);
                    }
                    else //Failed Login
                    {
                        //Logging
                        MyLogger.GetInstance().Info(" Failed Login --> Exiting Login Controller");

                        //Clearing session
                        Session.Clear();
                        return View("LoginFailure", user);
                    }
                }
                else
                {
                    //Clearing session
                    Session.Clear();
                    return View("LoginFailure", user);
                }
            }
            catch(Exception e)
            {
                //Logging
                MyLogger.GetInstance().Error("Exception in Login controller! " + e.Message);
                return Content("Exception in Login Controller " + e.Message);
            }            
        }

        /*
         * This method clears all session variables and redirects user to login page
         */
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Clear();

            FormsAuthentication.RedirectToLoginPage();
            return View("Index");
        }
       
    }
}