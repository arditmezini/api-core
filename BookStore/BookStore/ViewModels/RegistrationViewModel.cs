using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Models.Request;
using BookStore.Utility.AsyncCommands;
using BookStore.ViewModels.Base;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        #region Bindable Properties
        private string _firstname;
        public string FirstName
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value, nameof(FirstName),
                () => { RegisterCommand?.RaiseCanExecuteChanged(); });
        }

        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value, nameof(LastName),
                () => { RegisterCommand?.RaiseCanExecuteChanged(); });
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, nameof(Email),
                () => { RegisterCommand?.RaiseCanExecuteChanged(); });
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password),
                () => { RegisterCommand?.RaiseCanExecuteChanged(); });
        }

        private string _role;
        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value, nameof(Role),
                () => { RegisterCommand?.RaiseCanExecuteChanged(); });
        }
        #endregion

        public RegistrationViewModel(INavigationService navigationService, IDialogService dialogService,
            IAuthenticationService authenticationService)
            : base(navigationService, dialogService)
        {
            _authenticationService = authenticationService;

            RegisterCommand = new AsyncCommand(OnRegisterUser, CanRegister);
            GoToLoginPage = new AsyncCommand(OnLoginPage);
        }

        public override async Task InitializeAsync(object data)
        {
            var roles = await _authenticationService.GetRoles();
            Role = roles.FirstOrDefault().Name;
        }

        #region Commands

        public IAsyncCommand RegisterCommand { get; set; }
        private async Task OnRegisterUser()
        {
            _dialogService.ShowLoading();
            var register = new RegisterRequest
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Role = Role
            };
            var response = await _authenticationService.Register(register);
            if (response != null)
            {
                _dialogService.HideLoading();
                await _navigationService.NavigateToAsync<MainViewModel>();
            }
            _dialogService.HideLoading();
        }

        private bool CanRegister()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)
                || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                return false;
            return true;
        }

        public IAsyncCommand GoToLoginPage { get; set; }
        private async Task OnLoginPage()
        {
            await _navigationService.NavigateToAsync<LoginViewModel>();
        }

        #endregion
    }
}