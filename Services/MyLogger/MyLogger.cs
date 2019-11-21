using System;
using System.Collections.Generic;
using System.Text;

namespace Services.MyLogger
{
    public abstract class MyLogger : IMyLogger
    {
        public abstract void Debug(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");

        public abstract void Error(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");


        public abstract void Fatal(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");


        public abstract void Info(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");


        public abstract void Trace(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");


        public abstract void Warn(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");
    }
}
