using Activity1Part3.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Activity1Part3.Controllers
{
    public class TestLoggingService1Controller : Controller
    {

        readonly private ILogger logger;

        public TestLoggingService1Controller(ILogger logger) {
            this.logger = logger;
        }





        // GET: TestLoggingService1
        public String Index() {

            logger.Info("message");
            return ("message");
        }
    }
}