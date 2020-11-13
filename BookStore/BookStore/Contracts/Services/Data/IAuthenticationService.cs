using BookStore.Models.Request;
using BookStore.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        Task<UserResponse> Login(LoginRequest login);
        Task<UserResponse> Register(RegisterRequest register);
        Task<List<RoleResponse>> GetRoles();
        Task<bool> ValidateToken(string token);
        Task<UserResponse> Profile(string email);
        Task<bool> IsUserAuthenticated();
    }
}