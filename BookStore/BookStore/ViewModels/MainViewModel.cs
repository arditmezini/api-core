using BookStore.ViewModels.Base;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private MenuViewModel _menuViewModel;

        public MainViewModel(MenuViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
        }

        public MenuViewModel MenuViewModel
        {
            get => _menuViewModel;
            set
            {
                _menuViewModel = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            await Task.WhenAll
            (
                _menuViewModel.InitializeAsync(data)
                //_navigationService.NavigateToAsync<HomeViewModel>()
            );
        }
    }
}
