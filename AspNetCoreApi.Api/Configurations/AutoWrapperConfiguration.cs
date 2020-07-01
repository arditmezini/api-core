using AutoWrapper;
using Microsoft.AspNetCore.Builder;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// AutoWrapper Configuration
    /// </summary>
    public static class AutoWrapperConfiguration
    {
        /// <summary>
        /// Use AutoWrapper
        /// </summary>
        /// <param name="app"></param>
        public static void UseAutoWrapper(this IApplicationBuilder app)
        {
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                IsApiOnly = false,
                EnableResponseLogging = true,
                EnableExceptionLogging = true,
                UseCustomSchema = true
            });
        }
    }
}