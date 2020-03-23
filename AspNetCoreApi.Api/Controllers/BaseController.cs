using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetCoreApi.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper mapper;

        protected BaseController(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}