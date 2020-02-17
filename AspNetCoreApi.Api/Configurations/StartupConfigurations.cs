using AspNetCoreApi.Api.Filters;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Common.Emails;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

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
                    new Info
                    {
                        Title = "ASP.NET Core Web API",
                        Version = "v1",
                        Description = "ASP.NET Core Web API",
                        Contact = new Contact
                        {
                            Name = "API Support",
                            Email = "support@api.com",
                            Url = ""
                        },
                        License = new License
                        {
                            Name = "Apache 2.0",
                            Url = "http://www.apache.org/licenses/LICENSE-2.0.html"
                        },
                        TermsOfService = "TOS - Url"
                    });

                //sg.SwaggerBasicAuth();
                sg.SwaggerBearerAuth();

            });
        }

        private static void SwaggerBasicAuth(this SwaggerGenOptions sgo)
        {
            sgo.AddSecurityDefinition("Basic", new BasicAuthScheme
            {
                Type = "basic",
                Description = "Basic authentication for API"
            });

            sgo.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "basic", new string[] { } }
                });
        }

        private static void SwaggerBearerAuth(this SwaggerGenOptions sgo)
        {
            sgo.AddSecurityDefinition("Bearer", new ApiKeyScheme
            {
                Name = "Authorization",
                Description = "Please enter into field the word 'Bearer' following by space and JWT token",
                In = "header",
                Type = "apiKey"
            });

            sgo.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
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

        #region MVC Config

        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ClockSkew = TimeSpan.Zero
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