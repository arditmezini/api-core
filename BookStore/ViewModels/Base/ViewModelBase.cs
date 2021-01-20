using BookStore.Contracts.Services.General;
using System;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Base
{
    public class ViewModelBase : NotifyPropertyChangedBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ViewModelBase(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
