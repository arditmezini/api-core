using BookStore.Constants;
using BookStore.Contracts.Repository;
using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Models.Dto;
using System.Threading.Tasks;

namespace BookStore.Services.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        private readonly IConnectionService _connectionService;

        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService, IConnectionService connectionService)
        {
            _genericRepository = genericRepository;
            _settingsService = settingsService;
            _connectionService = connectionService;
        }

        public async Task<UserDto> Login(LoginDto login)
        {
            var response = await _genericRepository.Post<UserDto, LoginDto>(ApiConstants.AccountLogin, login);
            if (response != null)
            {
                _settingsService.Token = response.Token;
            }
            return response;
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrWhiteSpace(_settingsService.Token) &&
                   _connectionService.IsConnected;
        }
    }
}
