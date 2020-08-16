using BibleVerseSearchApp.Models;
using BibleVerseSearchApp.Services.Business;
using BibleVerseSearchApp.Services.Data;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/* Mark Pratt
 * 8/12/2020
 */

namespace BibleVerseSearchApp.Controllers
{
    public class SearchController : Controller
    {
        // logger
        private static Logger logger = LogManager.GetLogger("myAppLoggerRules");


        // GET: Search
        public ActionResult Index()
        {
            return View("Search");
        }


        // Method: Search
        // controller method to search for a verse
        // if the verse is found, send it to the view to be displayed

        public ActionResult Search(Verse verse) {
            SecurityService securityService = new SecurityService();
            bool success = securityService.Authenticate(verse);

            
            Verse foundVerse = securityService.ReturnVerse();

            if (success) {
                logger.Info("Verse Found!");
                return View("VerseFound", foundVerse);
            }
            else {
                logger.Info("Verse not found.");
                return View("VerseNotFound");
            }

        }

    }
}