using Activity_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace Activity_3.Controllers
{
    public class CustomerController : Controller
    {

        Customer customer;
        List<Customer> customerList;

        public CustomerController() {
            customerList = new List<Customer>();
            customerList.Add(new Customer(0, "Mark", 21));
            customerList.Add(new Customer(1, "Johnny", 33));
            customerList.Add(new Customer(2, "Odysseus", 27));
            customerList.Add(new Customer(3, "Frank", 50));
            customerList.Add(new Customer(4, "Karen", 45));

        }

        // GET: Customer
        public ActionResult Index()
        {
            Tuple<List<Customer>, Customer> customerTuple;
            customerTuple = new Tuple<List<Customer>, Customer>(customerList, customerList[0]);

            return View("Customer", customerTuple);
        }

        [HttpPost]
        public string GetMoreInfo(string customerID) {
            return customerID;
        }


        [HttpPost]
        public ActionResult OnSelectCustomer(string CustomerNumber) {
            Tuple<List<Customer>, Customer> tuple;
            tuple = new Tuple<List<Customer>, Customer>(customerList, customerList[Int32.Parse(CustomerNumber)]);

            return PartialView("_CustomerDetails", customerList[Int32.Parse(CustomerNumber)]);
        }
    }
}