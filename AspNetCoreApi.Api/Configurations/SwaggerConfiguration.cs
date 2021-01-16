using AspNetCoreApi.Api.Filters.Swagger;
using AspNetCoreApi.Common.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// Swagger Configuration
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Configure Swagger
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(sg =>
            {
                sg.ResolveConflictingActions(descriptions =>
                {
                    return descriptions.First();
                });

                sg.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "ASP.NET 5 Web API",
                        Version = ApiConstants.Version1,
                        Description = "ASP.NET 5 Web API",
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

                sg.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Title = "ASP.NET 5 Web API",
                        Version = ApiConstants.Version2,
                        Description = "ASP.NET 5 Web API",
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

                sg.ConfigureSwaggerApiVersioning();

                sg.AddFluentValidationRules();

                sg.ConfigureComments();
            });
        }

        /// <summary>
        /// Use SwaggerWithUI
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void UseSwaggerWithUI(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
                c.DocExpansion(DocExpansion.None);

                if (!env.IsDevelopment())
                {
                    c.RoutePrefix = string.Empty;
                }
            });
        }

        /// <summary>
        /// Swagger Bearer Auth
        /// </summary>
        /// <param name="sgo"></param>
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

            sgo.OperationFilter<UnauthorizedResponsesOperationFilter>(securityScheme);
        }

        /// <summary>
        /// Swagger Api Versioning
        /// </summary>
        /// <param name="sgo"></param>
        private static void ConfigureSwaggerApiVersioning(this SwaggerGenOptions sgo)
        {
            sgo.OperationFilter<RemoveVersionFromParameterOperationFilter>();

            sgo.DocumentFilter<ReplaceVersionWithExactValueInPathDocumentFilter>();
        }

        /// <summary>
        /// XML comments
        /// </summary>
        /// <param name="sgo"></param>
        private static void ConfigureComments(this SwaggerGenOptions sgo)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            sgo.IncludeXmlComments(xmlPath);
        }
    }
}