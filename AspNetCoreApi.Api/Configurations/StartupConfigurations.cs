using AspNetCoreApi.Models.Dto.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspNetCoreApi.Api.Configurations
{
    public static class StartupConfigurations
    {
        public static T GetGeneric<T>(this IConfiguration configuration, string section)
        {
            T value = configuration.GetSection(section).Get<T>();
            return value;
        }

        #region MVC Config, FluentValidations and NewtonsoftJson

        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.RegisterValidators();
        }

        #endregion
    }
}