using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AutoMapper;

namespace AspNetCoreApi.Common.Mapping
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