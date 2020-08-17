using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Services.Utility
{
    public class MyLogger : ILogger
    {

        private static MyLogger instance;
        private static Logger logger;

        private MyLogger()
        {

        }

        public static MyLogger GetInstance()
        {
            if(instance == null)
            {
                instance = new MyLogger();
            }
            return instance;
        }

        private Logger GetLogger(String theLogger)
        {
            if(MyLogger.logger == null)
            {
                MyLogger.logger = LogManager.GetLogger(theLogger);
            }
            return MyLogger.logger;
        }

        public void Debug(string message, string arg = null)
        {
            if(arg == null)
            {
                GetLogger("myAppLoggerRule").Debug(message);
            }
            else
            {
                GetLogger("myAppLoggerRule").Debug(message, arg);
            }
        }

        public void Error(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRule").Error(message);
            }
            else
            {
                GetLogger("myAppLoggerRule").Error(message, arg);
            }
        }

        public void Info(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRule").Info(message);
            }
            else
            {
                GetLogger("myAppLoggerRule").Info(message, arg);
            }
        }

        public void Warning(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("myAppLoggerRule").Warn(message);
            }
            else
            {
                GetLogger("myAppLoggerRule").Warn(message, arg);
            }
        }
    }
}