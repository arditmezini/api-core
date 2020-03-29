using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// Swagger Configuration
    /// </summary>
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(sg =>
            {
                sg.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "ASP.NET Core Web API",
                        Version = "v1",
                        Description = "ASP.NET Core Web API",
                        Contact = new OpenApiContact
                        {
                            Name = "API Support",
                            Email = "support@api.com",
                            Url = new Uri("http://wwww.contact.com")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Apache 2.0",
                            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                        },
                        TermsOfService = new Uri("http://www.tos.com")
                    });

                sg.SwaggerBearerAuth();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                sg.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerWithUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DocExpansion(DocExpansion.None);
            });
        }

        private static void SwaggerBearerAuth(this SwaggerGenOptions sgo)
        {
            var securitySchemeDefinition = new OpenApiSecurityScheme
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Please enter into field the JWT token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http
            };
            sgo.AddSecurityDefinition("JWT - Auth", securitySchemeDefinition);

            OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "JWT - Auth",
                    Type = ReferenceType.SecurityScheme
                }
            };
            OpenApiSecurityRequirement securityRequirement = new OpenApiSecurityRequirement()
            {
                {securityScheme, new string[] { }},
            };
            sgo.AddSecurityRequirement(securityRequirement);
        }
    }
}