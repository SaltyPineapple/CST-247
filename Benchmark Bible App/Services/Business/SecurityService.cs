using BibleVerseSearchApp.Models;
using BibleVerseSearchApp.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/* Mark Pratt
 * 8/12/2020
 */

namespace BibleVerseSearchApp.Services.Business {
    public class SecurityService {
        SecurityDAO security = new SecurityDAO();

        // Returns bool value from function within SecurityDAO
        // bool is if verse exists
        public bool Authenticate(Verse verse) {
            return security.FindVerse(verse);
        } 

        // returns bool from function within SecurityDAO
        // bool is if the verse was successfully added
        public bool AddNewVerse(Verse verse) {
            return security.EnterVerse(verse);
        }

        // used to return verse from database
        public Verse ReturnVerse() {
            return security.ReturnVerse();
        }
    }

}