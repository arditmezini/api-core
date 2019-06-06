using AspNetCoreApi.Models.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCoreApi.Api.Configurations
{
    public static class StartupConfigurations
    {
        public static T GetGenericValue<T>(this IConfiguration configuration, string parameter)
        {
            T value = configuration.GetValue<T>(parameter);
            return value;
        }

        #region Swagger Config and Use

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Api Core",
                        Version = "v1",
                        Description = "Web API Asp.Net Core",
                        Contact = new Contact
                        {
                            Name = "Ardit Mezini"
                        }
                    });
            });
        }

        public static void UseSwaggerWithUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        #endregion

        #region Cors Config and Use

        public static void ConfigureCors(this IServiceCollection services, string policyName, string corsOrigin)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, builder => builder.WithOrigins(corsOrigin));
            });
        }

        public static void ConfigureCorsGlobally(this IServiceCollection services, string policyName)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(policyName));
            });
        }

        public static void UseCorsPolicy(this IApplicationBuilder app, string policyName)
        {
            app.UseCors(policyName);
        }

        #endregion
    }
}