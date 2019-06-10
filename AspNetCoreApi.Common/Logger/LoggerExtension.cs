using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace AspNetCoreApi.Common.Logger
{
    public static class LoggerExtension
    {
        public static void ConfigureNLogStartup(string logDirectory)
        {
            GlobalDiagnosticsContext.Set("appbasepath", logDirectory);
            var path = string.Concat(logDirectory, "/nlog.config");
            LogManager.LoadConfiguration(path);
        }

        public static void RegisterNLog(this IServiceCollection services)
        {
            services.AddTransient<ILogNLog, LogNLog>();
        }
    }
}
