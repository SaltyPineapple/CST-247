using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Services.Utility
{
    /*
     * This interface acts as a singleton pattern for our logger 
     */
    public interface ILogger
    {
        void Debug(String message, String arg = null);
        void Info(String message, String arg = null);
        void Warning(String message, String arg = null);
        void Error(String message, String arg = null);
    }
}