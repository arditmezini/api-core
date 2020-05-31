using BookStore.Contracts.Services.General;
using BookStore.Utility.AsyncCommands;
using BookStore.ViewModels.Base;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
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

        public LoginViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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

        }
        
        private async Task OnRegistrationPage()
        {
            await _navigationService.NavigateToAsync<RegistrationViewModel>();
        }

        #endregion
    }
}