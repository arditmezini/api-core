using Hangfire.Annotations;
using Hangfire.Dashboard;
using System.Security.Claims;

namespace AspNetCoreApi.Api.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private string role;

        public HangfireAuthorizationFilter(string role)
        {
            this.role = role;
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            #if DEBUG
                return true;
            #else
                var httpContext = context.GetHttpContext();
                var userRole = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                return userRole == role;
            #endif
        }
    }
}