using AspNetCoreApi.Models.Common.Configurations;
using AspNetCoreApi.Models.Common.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// JWT Configuration
    /// </summary>
    public static class JwtConfiguration
    {
        /// <summary>
        /// Configure Jwt
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetGeneric<JwtConfig>("JwtConfig");

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.JwtKey));

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
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtConfig.JwtIssuer,
                        ValidAudience = jwtConfig.JwtAudience,
                        IssuerSigningKey = key,
                    };

                    cfg.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["authorization"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/newshub"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Role.Admin, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, new[] { Role.Admin });
                });

                options.AddPolicy(Role.Manager, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, new[] { Role.Admin, Role.Manager });
                });

                options.AddPolicy(Role.User, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, new[] { Role.Admin, Role.Manager, Role.User });
                });
            });
        }
    }
}