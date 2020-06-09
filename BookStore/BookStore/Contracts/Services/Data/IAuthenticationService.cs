using BookStore.Models.Dto;
using System.Threading.Tasks;

namespace BookStore.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        Task<UserDto> Login(LoginDto login);
        bool IsUserAuthenticated();
    }
}