using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AutoMapper;

namespace AspNetCoreApi.Api.Mapping
{
    public class AuthorMapping : Profile
    {
        public AuthorMapping()
        {
            CreateMap<Author, AuthorDto>()
                .ReverseMap();
        }
    }
}