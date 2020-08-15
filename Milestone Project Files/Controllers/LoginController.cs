using Milestone2.Models;
using Milestone2.Services.Business;
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
            LoginModel login = new LoginModel();
            return View(login);
        }

        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            //Data validation
            if (ModelState.IsValid)
            {
                SecurityService service = new SecurityService();

                //Calling helper methods to check credentials
                bool flag = service.Authenticate(user);

                if (flag) //Succesful login
                {
                    //Saving user in session
                    System.Web.HttpContext.Current.Session["user"] = user;

                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                    return View("LoginSuccess", user);
                }
                else //Failed Login
                { 
                    return View("LoginFailure", user);
                }
            }
            else
            {
                return View("LoginFailure", user);
            }
        }

        //This method logs the user out. Still needs work, doesnt clea session correctly yet
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();


            return View("Index");
        }
       
    }
}