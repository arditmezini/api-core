using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Models.Response;
using BookStore.ViewModels.Base;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;
        
        private UserResponse _userResponse;
        public UserResponse UserResponse
        {
            get => _userResponse;
            set => SetProperty(ref _userResponse, value);
        }

        public ProfileViewModel(INavigationService navigationService, IDialogService dialogService, 
            IAuthenticationService authenticationService, ISettingsService settingsService)
            : base(navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public override async Task InitializeAsync(object data)
        {
            UserResponse = await _authenticationService.Profile(_settingsService.Username);
        }
    }
}