using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/* Mark Pratt
 * 8/12/2020
 */

namespace BibleVerseSearchApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        // Method: OnButtonClick
        // gets which button was pressed on home screen and returns the view accordingly

        [HttpPost]
        public ActionResult OnButtonClick(string buttonValue) {
            if (buttonValue == "search") {
                return View("~/Views/Search/Search.cshtml");
            }
            else if (buttonValue == "entry") {
                return View("~/Views/Entry/Entry.cshtml");
            }
            else {
                return View("Index");
            }
        }
    }
}