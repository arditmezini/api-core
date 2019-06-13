using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AutoMapper;

namespace AspNetCoreApi.Api.Mapping
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