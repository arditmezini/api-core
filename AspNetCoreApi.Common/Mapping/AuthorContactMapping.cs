using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AutoMapper;

namespace AspNetCoreApi.Common.Mapping
{
    public class AuthorContactMapping : Profile
    {
        public AuthorContactMapping()
        {
            CreateMap<AuthorContact, AuthorContactDto>()
                .ReverseMap();
        }
    }
}