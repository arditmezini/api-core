using BookStore.Contracts.Services.General;
using BookStore.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookStore.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(INavigationService navigationService)
            : base(navigationService)
        { }

        public ICommand GoToRegistrationPage => new Command(OnRegistrationPage);
        private async void OnRegistrationPage()
        {
            await _navigationService.NavigateToAsync<RegistrationViewModel>();
        }
    }
}