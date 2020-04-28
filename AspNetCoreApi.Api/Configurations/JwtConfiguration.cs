using AspNetCoreApi.Models.Common.Configurations;
using AspNetCoreApi.Models.Common.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

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
                        IssuerSigningKey = key,
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig.JwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = jwtConfig.JwtAudience,
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