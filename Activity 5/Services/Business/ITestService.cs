using Activity1Part3.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Activity1Part3.Services.Business {
    interface ITestService {

        private ILogger logger;

        [InjectionMethod]
        public void Initialize(ILogger logger) {
            this.logger = logger;
        }

        public void TestLogger() {
            logger.Info("Test Logging in TestService.TestLogger() invoked");
        }
    }
}
