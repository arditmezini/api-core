using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AutoMapper;

namespace AspNetCoreApi.Common.Mapping
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