using AspNetCoreApi.Dal.Core.Contracts;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AspNetCoreApi.Dal.Core
{
    public class LoggedInUser : ILoggedInUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Username => _httpContextAccessor?.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
    }
}
