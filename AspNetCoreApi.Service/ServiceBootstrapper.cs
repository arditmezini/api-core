using AspNetCoreApi.Service.Contracts;
using AspNetCoreApi.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Service
{
    public static class ServiceBootstrapper
    {
        public static void RegisterServicesDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IGeneralDataService, GeneralDataService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<IBookCategoryService, BookCategoryService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IHangfireJobService, HangfireJobService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<INewsService, NewsService>();
        }
    }
}