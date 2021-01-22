using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AutoMapper;

namespace AspNetCoreApi.Common.Mapping
{
    public class PublisherMapping : Profile
    {
        public PublisherMapping()
        {
            CreateMap<Publisher, PublisherDto>()
                .ReverseMap();
        }
    }
}