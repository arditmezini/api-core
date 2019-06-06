using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreApi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
            .UseStartup<Startup>();
    }
}