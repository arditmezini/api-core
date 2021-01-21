using AspNetCoreApi.Service.Contracts;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AspNetCoreApi.Service.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Username => _httpContextAccessor?.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
    }
}
