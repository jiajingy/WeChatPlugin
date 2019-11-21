using System;
using System.Collections.Generic;
using System.Text;

namespace Services.MyLogger
{
    public interface IMyLogger
    {
        void Debug(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");
        void Fatal(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");
        void Warn(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");
        void Info(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");
        void Error(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");
        void Trace(string logMsg, string appVariable1 = "", string appVariable2 = "", string appVariable3 = "");
    }
}
