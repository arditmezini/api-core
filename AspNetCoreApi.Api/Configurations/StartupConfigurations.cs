﻿using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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

                s.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                s.AddSecurityRequirement(security);
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
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApiContext>()
                .AddDefaultTokenProviders();
        }
        #endregion

        #region Jwt Configuration

        public static void ConfigureJwt(this IServiceCollection services, string jwtIssuer, string jwtKey)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
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
    }
}