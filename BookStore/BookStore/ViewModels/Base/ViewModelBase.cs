using BookStore.Contracts.Services.General;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Base
{
    public class ViewModelBase : NotifyPropertyChangedBase
    {
        public readonly INavigationService _navigationService;

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
