using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Repository.Library
{
    public class Log
    {
        public void logErrorMessage(string th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logInfoMessage(string th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
        public void logDebugMessage(string th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }
    }
}