using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace AspNetCoreApi.Api.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMapper mapper;
        protected string UserRole =>
            HttpContext.User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))?.Value;

        protected BaseController(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}