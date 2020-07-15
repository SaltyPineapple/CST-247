using Activity2Part1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Activity2Part1.Controllers
{
    public class TestController : Controller
    {
        private List<UserModel> userList;
        // GET: Test
        public ActionResult Index()
        {

            userList = new List<UserModel>() {
                new UserModel() {
                    Name = "Mark", Phone = "555-123-4567", Email = "mark@mark.com"
                },
                new UserModel() {
                    Name = "Kyle", Phone = "555-987-6543", Email = "Kyle@scrabbleplayer.com"
                },
                new UserModel() {
                    Name = "Tommy", Phone = "555-246-8101", Email = "Tommy@bigNerd.com"
                }
            };

            return View("Test", userList);
        }
    }
}