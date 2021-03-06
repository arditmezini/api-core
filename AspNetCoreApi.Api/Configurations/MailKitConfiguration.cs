﻿using AspNetCoreApi.Models.Common.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// MailKit Configuration
    /// </summary>
    public static class MailKitConfiguration
    {
        /// <summary>
        /// Configure MailKit
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureMailKit(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfiguration = configuration.GetGeneric<EmailConfiguration>("EmailConfiguration");

            services.AddSingleton(emailConfiguration);
        }
    }
}
