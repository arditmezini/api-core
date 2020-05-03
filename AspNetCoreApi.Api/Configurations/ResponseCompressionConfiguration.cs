using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace AspNetCoreApi.Api.Configurations
{
    /// <summary>
    /// Response Compression
    /// </summary>
    public static class ResponseCompressionConfiguration
    {
        /// <summary>
        /// Configure Response Compression
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureResponseCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
        }
    }
}