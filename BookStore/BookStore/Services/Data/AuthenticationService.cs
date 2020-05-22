using BookStore.Contracts.Services.Data;

namespace BookStore.Services.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService() { }

        public bool IsUserAuthenticated()
        {
            return false;
        }
    }
}
