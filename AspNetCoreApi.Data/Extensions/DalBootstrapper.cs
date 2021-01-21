using AspNetCoreApi.Dal.Core;
using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Data.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreApi.Dal.Extensions
{
    public static class DalBootstrapper
    {
        public static void AddDbContextWithLazyLoading(this IServiceCollection services, Action<DbContextOptionsBuilder> options, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), contextLifetime));
            services.AddDbContext<ApiContext>(options, contextLifetime, optionsLifetime);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ILoggedInUser, LoggedInUser>();
        }
    }
}