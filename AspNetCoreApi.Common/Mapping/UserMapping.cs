using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AutoMapper;

namespace AspNetCoreApi.Common.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}