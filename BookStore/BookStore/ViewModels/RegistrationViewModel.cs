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
        {
            GoToLoginPage = new AsyncCommand(OnLoginPage);
        }

        #region Commands

        public IAsyncCommand GoToLoginPage { get; set; }
        private async Task OnLoginPage()
        {
            await _navigationService.NavigateToAsync<LoginViewModel>();
        }

        #endregion
    }
}