using AspNetCoreApi.Common.Logger;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace AspNetCoreApi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggerHelper.ConfigureNLogStartup();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((builderContext, config) =>
            {
                var env = builderContext.HostingEnvironment;

                config.SetBasePath(env.ContentRootPath);
                config.AddJsonFile(
                    "appsettings.json",
                    optional: false,
                    reloadOnChange: true);
                config.AddJsonFile(
                    $"appsettings.{env.EnvironmentName}.json",
                    optional: true,
                    reloadOnChange: true);
            })
            .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
            .UseNLog()
            .UseStartup<Startup>();
    }
}