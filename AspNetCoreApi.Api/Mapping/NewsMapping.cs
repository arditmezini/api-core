using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AutoMapper;

namespace AspNetCoreApi.Api.Mapping
{
    public class NewsMapping : Profile
    {
        public NewsMapping()
        {
            CreateMap<News, NewsDto>()
                .ReverseMap();
        }
    }
}