using Registration.Models;
using Registration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View("Register");
        }


        // Check for existing user
        // if user already exists, ask to make new user
        // otherwise register new user
        [HttpPost]
        public ActionResult Register(PlayerModel model) {
            Security registration = new Security();
            if (registration.existingUser(model)) {
                return View("RegisterFailed");
            }
            else {
                registration.addNewUser(model);
                return View("RegisterPassed");
            }
        }
    }
}