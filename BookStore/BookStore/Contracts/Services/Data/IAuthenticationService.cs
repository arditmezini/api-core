using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated();
    }
}
