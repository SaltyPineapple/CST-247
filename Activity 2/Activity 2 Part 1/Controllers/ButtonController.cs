using Activity2Part1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Activity2Part1.Controllers
{
    public class ButtonController : Controller
    {

        private static List<ButtonModel> buttons = new List<ButtonModel>();
        // GET: Button
        public ActionResult Index()
        {
            buttons.Add(new ButtonModel(false));
            buttons.Add(new ButtonModel(true));

            return View("Button", buttons);
        }

        public ActionResult OnButtonClick(String mine) {
            if(mine == "1") {
                buttons[0].State = false;
            }
            else {
                buttons[1].State = true;
            }


            return View("Button", buttons);
        }
    }
}