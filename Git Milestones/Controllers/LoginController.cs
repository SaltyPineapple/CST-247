using Milestone2.Models;
using Milestone2.Services.Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index(LoginModel user)
        {
            //Data validation
            if (ModelState.IsValid)
            {
                SecurityService service = new SecurityService();

                //Calling helper methods to check credentials
                bool flag = service.Authenticate(user);

                if (flag) //Succesful login
                {
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
       
    }
}