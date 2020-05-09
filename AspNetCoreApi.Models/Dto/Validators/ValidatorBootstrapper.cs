using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public static class ValidatorBootstrapper
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<BookDto>, BookValidator>();
            services.AddTransient<IValidator<BookCategoryDto>, BookCategoryValidator>();
            services.AddTransient<IValidator<AuthorDto>, AuthorValidator>();
            services.AddTransient<IValidator<AuthorContactDto>, AuthorContactValidator>();
            services.AddTransient<IValidator<PublisherDto>, PublisherValidator>();
            services.AddTransient<IValidator<RegisterDto>, RegisterValidator>();
            services.AddTransient<IValidator<LoginDto>, LoginValidator>();
        }
    }
}