using Hangfire.Annotations;
using Hangfire.Dashboard;

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
            //todo
            return false;
        #endif
        }
    }
}