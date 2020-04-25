using AspNetCoreApi.Models.Dto.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// MVC Config, FluentValidations and NewtonsoftJson
    /// </summary>
    public static class MvcConfigurations
    {
        /// <summary>
        /// Configure Mvc
        /// </summary>
        /// <param name="services"></param>
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
    }
}