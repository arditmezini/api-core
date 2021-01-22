using AspNetCoreApi.Common.Mapping;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AuthorContactMapping());
                mc.AddProfile(new AuthorMapping());
                mc.AddProfile(new BookCategoryMapping());
                mc.AddProfile(new BookMapping());
                mc.AddProfile(new CountryMapping());
                mc.AddProfile(new NewsMapping());
                mc.AddProfile(new PublisherMapping());
                mc.AddProfile(new RolesMapping());
                mc.AddProfile(new UserMapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
