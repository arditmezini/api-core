using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreApi.Api.Configurations
{
    public static class SqlCacheConfiguration
    {
        public static void ConfigureSqlCache(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = connectionString;
                options.DefaultSlidingExpiration = TimeSpan.FromMinutes(8); // default 20 min
                options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(20); // default 30 min
                options.SchemaName = "dbo";
                options.TableName = "SqlCache";
            });
        }
    }
}