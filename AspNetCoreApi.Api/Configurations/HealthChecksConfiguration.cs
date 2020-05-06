using AspNetCoreApi.Models.Common;
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
using HealthCheckService = AspNetCoreApi.Models.Common.HealthCheckService;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// HealthChecks Configuration
    /// </summary>
    public static class HealthChecksConfiguration
    {
        public const string HEALTH_QUERY = "SELECT 1";
        public static TimeSpan Timeout = new TimeSpan(0, 0, 0, 30, 0);

        /// <summary>
        /// Configure HealthChecks
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheck = configuration.GetGeneric<HealthChecksConfig>("HealthCheck");
            var connHangfire = configuration.GetConnectionString("HangfireConnection");
            var connDb = configuration.GetConnectionString("DefaultConnection");

            services.AddHealthChecks()
                .AddDiskStorageHealthCheck((diskOptions) => 
                {
                    diskOptions.AddDrive(healthCheck.Drive.Letter, healthCheck.Drive.MinSpace);
                }, "Application Drive", HealthStatus.Degraded, new[] { "drive_check" }, Timeout)
                .AddSqlServer(connDb, HEALTH_QUERY, "Application Database", HealthStatus.Unhealthy, new[] { "application_database_check" }, Timeout)
                .AddSqlServer(connHangfire, HEALTH_QUERY, "Hangfire Database", HealthStatus.Unhealthy, new[] { "hangfire_database_check" }, Timeout)
                .AddHangfire((hangfireOptions) =>
                {
                    hangfireOptions.MinimumAvailableServers = 1;
                    hangfireOptions.MaximumJobsFailed = 1;
                }, "Hangfire Server", HealthStatus.Unhealthy, new[] { "hangfire_server_check" }, Timeout);
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
                var result = JsonConvert.SerializeObject(new HealthCheckDto
                {
                    HealthCheckSummary = new HealthCheckSummary
                    {
                        Status = rpt.Status.ToString(),
                        Duration = rpt.TotalDuration,
                    },
                    HealthCheckServices = rpt.Entries.Select(e => new HealthCheckService
                    {
                        Service = e.Key,
                        Tags = string.Join(",", e.Value.Tags),
                        Status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                        Duration = e.Value.Duration
                    })
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