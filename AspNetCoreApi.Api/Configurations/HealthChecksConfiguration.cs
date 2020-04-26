using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Common.Configurations;
using HealthChecks.Hangfire;
using HealthChecks.System;
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
        /// <summary>
        /// Configure HealthChecks
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheck = configuration.GetGeneric<HealthChecksConfig>("HealthCheck");
            var connHangfire = configuration.GetConnectionString("HangfireConnection");
            var connDb = configuration.GetConnectionString("DefaultConnection");

            services.AddHealthChecks()
                .AddDiskStorageHealthCheck(delegate (DiskStorageOptions options)
                {
                    options.AddDrive(healthCheck.Drive.Letter, healthCheck.Drive.MinSpace);
                }, "Application Drive", HealthStatus.Degraded, tags: new[] { "drive_check" }, new TimeSpan(0, 0, 0, 30, 0))
                .AddSqlServer(connDb, "SELECT 1", "Application Database", HealthStatus.Unhealthy, tags: new[] { "application_database_check" }, new TimeSpan(0, 0, 0, 30, 0))
                .AddSqlServer(connHangfire, "SELECT 1", "Hangfire Database", HealthStatus.Unhealthy, tags: new[] { "hangfire_database_check" }, new TimeSpan(0, 0, 0, 30, 0))
                .AddHangfire(delegate (HangfireOptions options)
                {
                    options.MinimumAvailableServers = 1;
                    options.MaximumJobsFailed = 1;
                }, "Hangfire Server", HealthStatus.Unhealthy, tags: new[] { "hangfire_server_check" }, new TimeSpan(0, 0, 0, 30, 0));
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