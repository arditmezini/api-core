using BookStore.Contracts.Services.General;
using BookStore.ViewModels.Base;

namespace BookStore.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigationService navigationService, IDialogService dialogService)
            : base(navigationService, dialogService)
        { }
    }
}