using AspNetCoreApi.Models.Common.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
                        ValidAudience = jwtConfig.JwtIssuer,
                    };
                });
        }
    }
}