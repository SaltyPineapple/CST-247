using BibleVerseSearchApp.Models;
using BibleVerseSearchApp.Services.Business;
using NLog;
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
    public class EntryController : Controller
    {
        // Logger
        private static Logger logger = LogManager.GetLogger("myAppLoggerRules");

        // GET: Entry
        public ActionResult Index()
        {
            return View("Entry");
        }


        // Method: Entry
        // Calls the security service to enter a verse into the database
        // if the verse does not already exist
        // enter in the verse
        public ActionResult Entry(Verse verse) {
            SecurityService security = new SecurityService();
            if (security.Authenticate(verse)) {
                logger.Info("Verse entry denied. Verse may already exist");
                return View("EntryDenied");
            }
            else {
                if (security.AddNewVerse(verse)) {
                    logger.Info("Verse entry success!");
                    return View("EntryPermitted", verse);
                }
                else {
                    logger.Info("Verse entry denied. Could be invalid parameters");
                    return View("EntryDenied");
                }
            }

        }
    }
}