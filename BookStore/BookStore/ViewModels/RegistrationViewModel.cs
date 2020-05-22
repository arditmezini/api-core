using BookStore.Contracts.Services.General;
using BookStore.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookStore.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public RegistrationViewModel(INavigationService navigationService) 
            : base(navigationService)
        { }

        public ICommand GoToLoginPage => new Command(OnLoginPage);
        private async void OnLoginPage()
        {
            await _navigationService.NavigateToAsync<LoginViewModel>();
        }
    }
}