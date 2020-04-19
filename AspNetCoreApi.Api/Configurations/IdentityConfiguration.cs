﻿using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// Identity Configuration
    /// </summary>
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApiContext>()
            .AddDefaultTokenProviders();
        }
    }
}