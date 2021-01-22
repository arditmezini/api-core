using AspNetCoreApi.Models.Common.Configurations;
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
        /// <summary>
        /// Configure Cors
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var cors = configuration.GetGeneric<CorsOptions>("CorsOptions");

            services.AddCors(options =>
            {
                options.AddPolicy(cors.PolicyName,
                    builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins(cors.CorsOrigin)
                );
            });
        }

        /// <summary>
        /// Use CorsPolicy
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseCorsPolicy(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseRouting();

            var cors = configuration.GetGeneric<CorsOptions>("CorsOptions");
            app.UseCors(cors.PolicyName);
        }
    }
}