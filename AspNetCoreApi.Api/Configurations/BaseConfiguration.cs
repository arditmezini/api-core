using Microsoft.Extensions.Configuration;

namespace AspNetCoreApi.Api.Configurations
{
    public static class BaseConfiguration
    {
        public static T GetGeneric<T>(this IConfiguration configuration, string section)
        {
            T value = configuration.GetSection(section).Get<T>();
            return value;
        }
    }
}