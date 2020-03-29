using AspNetCoreApi.Models.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// Cors Configuration
    /// </summary>
    public static class CorsConfiguration
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var cors = configuration.GetGeneric<CorsOptions>("CorsOptions");

            services.AddCors(options =>
            {
                options.AddPolicy(cors.PolicyName,
                    builder => builder.WithOrigins(cors.CorsOrigin));
            });
        }

        public static void UseCorsPolicy(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseRouting();

            var cors = configuration.GetGeneric<CorsOptions>("CorsOptions");
            app.UseCors(cors.PolicyName);
        }
    }
}
