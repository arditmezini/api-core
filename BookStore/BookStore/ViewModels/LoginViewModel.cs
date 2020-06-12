using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Models.Dto;
using BookStore.Utility.AsyncCommands;
using BookStore.ViewModels.Base;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        #region Bindable Properties

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value, nameof(Username),
                    () => { SignInCommand?.RaiseCanExecuteChanged(); });
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password),
                    () => { SignInCommand?.RaiseCanExecuteChanged(); });
        }

        #endregion

        public LoginViewModel(INavigationService navigationService, IDialogService dialogService,
            IAuthenticationService authenticationService)
            : base(navigationService, dialogService)
        {
            _authenticationService = authenticationService;

            SignInCommand = new AsyncCommand(OnSignIn, CanSignIn);
            GoToRegistrationPage = new AsyncCommand(OnRegistrationPage);
        }

        #region Commands

        public IAsyncCommand SignInCommand { get; set; }
        public IAsyncCommand GoToRegistrationPage { get; set; }

        private bool CanSignIn()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                return false;
            return true;
        }

        private async Task OnSignIn()
        {
            _dialogService.ShowLoading();
            var login = new LoginDto { Email = Username, Password = Password };
            var response = await _authenticationService.Login(login);
            if (response != null)
            {
                _dialogService.HideLoading();
                await _navigationService.NavigateToAsync<HomeViewModel>();
            }
        }

        private async Task OnRegistrationPage()
        {
            await _navigationService.NavigateToAsync<RegistrationViewModel>();
        }

        #endregion
    }
}