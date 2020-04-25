using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Common.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Mime;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// HealthChecks Configuration
    /// </summary>
    public static class HealthChecksConfiguration
    {
        /// <summary>
        /// Configure HealthChecks
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<ApiContext>();
        }

        /// <summary>
        /// Use HealthChecks
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseHealthChecks(this IApplicationBuilder app, IConfiguration configuration)
        {
            var config = configuration.GetGeneric<HealthChecksConfig>("HealthCheck");

            var options = new HealthCheckOptions();
            options.ResultStatusCodes[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable;

            options.ResponseWriter = async (ctx, rpt) =>
            {
                var result = JsonConvert.SerializeObject(new
                {
                    status = rpt.Status.ToString(),
                    services = rpt.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                },
                Formatting.None,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                ctx.Response.ContentType = MediaTypeNames.Application.Json;

                await ctx.Response.WriteAsync(result);
            };

            app.UseHealthChecks(config.Url, options);
        }
    }
}