using AspNetCoreApi.Api.Filters.Hangfire;
using AspNetCoreApi.Models.Common.Identity;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// Hangfire Configuration
    /// </summary>
    public static class HangfireConfiguration
    {
        /// <summary>
        /// Configure Hangfire
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HangfireConnection");

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseRecommendedSerializerSettings();

                var options = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = false,
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };
                config.UseSqlServerStorage(connectionString, options);
            });
        }

        /// <summary>
        /// Use Hangfire
        /// </summary>
        /// <param name="app"></param>
        public static void UseHangfire(this IApplicationBuilder app)
        {
            var options = new DashboardOptions
            {
                Authorization = new IDashboardAuthorizationFilter[]
                {
                    new HangfireAuthorizationFilter(Role.Admin)
                }
            };
            app.UseHangfireDashboard("/hangfire", options);

            app.UseHangfireServer();
        }
    }
}