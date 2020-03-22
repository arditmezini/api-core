using AspNetCoreApi.Api.Filters;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Common.Emails;
using AspNetCoreApi.Models.Dto.Validators;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace AspNetCoreApi.Api.Configurations
{
    public static class StartupConfigurations
    {
        public static T GetGeneric<T>(this IConfiguration configuration, string section)
        {
            T value = configuration.GetSection(section).Get<T>();
            return value;
        }

        #region Swagger Config and Use

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

        public static void UseSwaggerWithUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DocExpansion(DocExpansion.None);
            });
        }

        #endregion

        #region Cors Config and Use

        public static void ConfigureCors(this IServiceCollection services, string policyName, string corsOrigin)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder => builder.WithOrigins(corsOrigin));
            });
        }

        public static void UseCorsPolicy(this IApplicationBuilder app, string policyName)
        {
            app.UseRouting();

            app.UseCors(policyName);
        }

        #endregion

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

        #region Identity Configuration

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

        #endregion

        #region Jwt Configuration

        public static void ConfigureJwt(this IServiceCollection services, string jwtIssuer, string jwtKey)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                    };
                });
        }

        #endregion

        #region MailKit

        public static void ConfigureMailKit(this IServiceCollection services, EmailConfiguration emailConfiguration)
        {
            services.AddSingleton(emailConfiguration);
        }

        #endregion

        #region Hangfire
        public static void ConfigureHangfire(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseRecommendedSerializerSettings();

                var options = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = false,
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };
                config.UseSqlServerStorage(connectionString, options);
            });
        }

        public static void UseHangfire(this IApplicationBuilder app)
        {
            var options = new DashboardOptions
            {
                Authorization = new IDashboardAuthorizationFilter[]
                {
                    new HangfireAuthorizationFilter(Role.Admin)
                }
            };
            app.UseHangfireDashboard("/hangfire", options);

            app.UseHangfireServer();
        }
        #endregion
    }
}