using Milestone2.Models;
using Registration.Models;
using Registration.Services;
using Registration.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebSockets;

/* Mark Pratt
 * 
 */

namespace Registration.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register

        [HttpGet]
        public ActionResult Index()
        {
            //Logging
            MyLogger.GetInstance().Info(" Entering Register Controller");

            return View("Register");
        }


        /*
         * This method checks if user already exists, if not then user is created
         */
        [HttpPost]
        public ActionResult Register(PlayerModel model) {

            try
            {
                Security registration = new Security();

                //User exists already
                if (registration.existingUser(model))
                {
                    return View("RegisterFailed");
                }
                else
                {
                    registration.addNewUser(model);

                    //Saving user in session
                    System.Web.HttpContext.Current.Session["user"] = new LoginModel(model.Username, model.Password);

                    FormsAuthentication.RedirectFromLoginPage(model.Username, false);
                    return View("RegisterPassed");
                }
            }
            catch(Exception e)
            {
                MyLogger.GetInstance().Info(" Exception inside Register -->" + e.Message);
                return View("Index");
            }            
        }
    }
}