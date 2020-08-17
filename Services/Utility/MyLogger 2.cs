using NLog;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace Activity1Part3.Services.Utility {
    public class MyLogger2 : ILogger {

        // singleton design pattern. single insance
        static MyLogger2 instance;
        private Logger logger;

        // empty private constructor
        

        public static MyLogger2 GetInstance() {
            if (instance == null) {
                instance = new MyLogger2();
            }
            return instance;
        }


        private Logger GetLogger(string theLogger) {
            if(logger == null) {
                logger = LogManager.GetLogger(theLogger);
            }
            return logger;
        }



        public void Debug(string message, string arg = null) {
            if(arg == null) {
                GetLogger("myAppLoggerRules").Debug(message);
            }
            else {
                GetLogger("myAppLoggerRules").Debug(message, arg);
            }
        }

        public void Error(string message, string arg = null) {
            if (arg == null) {
                GetLogger("myAppLoggerRules").Error(message);
            }
            else {
                GetLogger("myAppLoggerRules").Error(message, arg);
            }
        }

        public void Info(string message, string arg = null) {
            if (arg == null) {
                GetLogger("myAppLoggerRules").Info(message);
            }
            else {
                GetLogger("myAppLoggerRules").Info(message, arg);
            }
        }

        public void Warning(string message, string arg = null) {
            if (arg == null) {
                GetLogger("myAppLoggerRules").Warn(message);
            }
            else {
                GetLogger("myAppLoggerRules").Warn(message, arg);
            }
        }
    }
}