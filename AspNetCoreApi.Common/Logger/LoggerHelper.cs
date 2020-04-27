using NLog;
using NLog.Web;
using System.IO;

namespace AspNetCoreApi.Common.Logger
{
    public static class LoggerHelper
    {
        public static void ConfigureNLogStartup()
        {
            //Set base path for NLog
            GlobalDiagnosticsContext.Set("appbasepath", Directory.GetCurrentDirectory());

            //Read the config file and get the logger class
            NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }
    }
}