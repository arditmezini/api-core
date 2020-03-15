using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public static class ValidatorBootstrapper
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<BookCategoryDto>, BookCategoryValidator>();
            services.AddTransient<IValidator<AuthorDto>, AuthorValidator>();
        }
    }
}
