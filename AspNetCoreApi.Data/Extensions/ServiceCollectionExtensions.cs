using AspNetCoreApi.Dal.Core;
using AspNetCoreApi.Dal.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreApi.Dal.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContextWithLazyLoading<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TDbContext : DbContext
        {
            AddServices<TDbContext>(services, options, contextLifetime);
            services.AddDbContext<TDbContext>(options, contextLifetime, optionsLifetime);
        }

        public static void AddDbContextPoolWithLazyLoading<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, int poolSize = 128) where TDbContext : DbContext
        {
            AddServices<TDbContext>(services, options, contextLifetime);
            services.AddDbContextPool<TDbContext>(options, poolSize);
        }

        private static void AddServices<TDbContext>(IServiceCollection services, Action<DbContextOptionsBuilder> options, ServiceLifetime contextLifetime = ServiceLifetime.Scoped) where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            //todo : optionsBuilder.UseLazyLoadingProxies();
            options(optionsBuilder);
            services.Add(new ServiceDescriptor(typeof(IUnitOfWork<TDbContext>), typeof(UnitOfWork<TDbContext>), contextLifetime));
            services.Add(new ServiceDescriptor(typeof(IRepository<,>), typeof(Repository<,>), contextLifetime));
        }
    }
}
