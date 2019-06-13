using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AutoMapper;

namespace AspNetCoreApi.Api.Mapping
{
    public class CountryMapping : Profile
    {
        public CountryMapping()
        {
            CreateMap<Countries, CountriesDto>()
                .ReverseMap();
        }
    }
}