using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.MyLogger
{
    public class FileLogger : MyLogger
    {
        private ILogger logger;

        public FileLogger(string NLogTargetName)
        {
            logger = LogManager.GetLogger(NLogTargetName);
        }

        private string ConstructAppVariables(string appVariable1, string appVariable2, string appVariable3)
        {
            return String.Format(@"(v1: {0} | v2: {1} | v3: {2}) --- ", appVariable1, appVariable2, appVariable3);
        }

        public override void Debug(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "")
        {
            logger.Debug(ConstructAppVariables(appVariable1, appVariable2, appVariable3) + logMsg);

        }

        public override void Error(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "")
        {
            logger.Error(ConstructAppVariables(appVariable1, appVariable2, appVariable3) + logMsg);
        }

        public override void Fatal(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "")
        {
            logger.Fatal(ConstructAppVariables(appVariable1, appVariable2, appVariable3) + logMsg);
        }

        public override void Info(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "")
        {
            logger.Info(ConstructAppVariables(appVariable1, appVariable2, appVariable3) + logMsg);
        }

        public override void Trace(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "")
        {
            logger.Trace(ConstructAppVariables(appVariable1, appVariable2, appVariable3) + logMsg);
        }

        public override void Warn(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "")
        {
            logger.Warn(ConstructAppVariables(appVariable1, appVariable2, appVariable3) + logMsg);
        }
    }
}
