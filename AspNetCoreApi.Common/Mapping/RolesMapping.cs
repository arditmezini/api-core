﻿using AspNetCoreApi.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreApi.Common.Mapping
{
    public class RolesMapping : Profile
    {
        public RolesMapping()
        {
            CreateMap<IdentityRole, RoleDto>()
                .ReverseMap();
        }
    }
}