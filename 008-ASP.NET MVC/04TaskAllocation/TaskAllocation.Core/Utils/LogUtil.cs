using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAllocation.Core.Utils
{
    public static class LogUtil
    {
        private static readonly ILog log = LogManager.GetLogger("System");

        public static void Info(string logInfo) 
        {
            log.Info(logInfo);
        }

        public static void Error(string logInfo)
        {
            log.Error(logInfo);
        }
    }
}
