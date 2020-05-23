using BookStore.Contracts.Services.General;
using BookStore.Utility.AsyncCommands;
using BookStore.ViewModels.Base;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(INavigationService navigationService)
            : base(navigationService)
        { }

        public IAsyncCommand GoToRegistrationPage => new AsyncCommand(OnRegistrationPage);
        private async Task OnRegistrationPage()
        {
            await _navigationService.NavigateToAsync<RegistrationViewModel>();
        }
    }
}