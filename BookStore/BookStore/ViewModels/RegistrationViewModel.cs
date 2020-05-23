using BookStore.Contracts.Services.General;
using BookStore.Utility.AsyncCommands;
using BookStore.ViewModels.Base;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public RegistrationViewModel(INavigationService navigationService) 
            : base(navigationService)
        { }

        public IAsyncCommand GoToLoginPage => new AsyncCommand(OnLoginPage);
        private async Task OnLoginPage()
        {
            await _navigationService.NavigateToAsync<LoginViewModel>();
        }
    }
}