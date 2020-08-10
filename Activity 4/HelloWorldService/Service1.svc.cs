using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HelloWorldService {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1 {
        public string SayHello() {
            return "Hey i am saying hello";
        }

        public string GetData(string value) {
            return "I now own " + value + " houses";
        }

        public HelloObject GetModelObj(string id) {
            HelloObject helloObject = new HelloObject();

            if(int.Parse(id) > 0) {
                helloObject.helloBool = true;
                helloObject.helloMessage = "Hey this is a positive message!";
            }
            else {
                helloObject.helloBool = false;
                helloObject.helloMessage = "Hello this is a negative message.";
            }
            return helloObject;

        }
    }
}
