using AspNetCoreApi.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Service
{
    public static class ServiceBootstrapper
    {
        public static void RegisterServicesDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IPublisherService, PublisherService>();
        }
    }
}
