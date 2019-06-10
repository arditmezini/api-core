using NLog;
using System;

namespace AspNetCoreApi.Common.Logger
{
    public class LogNLog : ILogNLog
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public LogNLog()
        { }

        public void Debug(string message) => logger.Debug(message);

        public void Error(string message) => Error(null, message);

        public void Error(Exception exception, string message) => logger.Error(exception, message);

        public void Information(string message) => logger.Info(message);

        public void Warning(string message) => logger.Warn(message);
    }
}