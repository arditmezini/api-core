using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AutoMapper;

namespace AspNetCoreApi.Api.Mapping
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