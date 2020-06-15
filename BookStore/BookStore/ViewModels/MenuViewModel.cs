using BookStore.Constants;
using BookStore.Contracts.Services.General;
using BookStore.Models.Dto;
using BookStore.Models.Enum;
using BookStore.Utility.AsyncCommands;
using BookStore.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<MainMenuItemDto> _menuItems;
        public ObservableCollection<MainMenuItemDto> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private readonly ISettingsService _settingsService;
        public MenuViewModel(INavigationService navigationService, IDialogService dialogService,
            ISettingsService settingsService)
            : base(navigationService, dialogService)
        {
            _settingsService = settingsService;

            MenuItemTappedCommand = new AsyncCommand<MainMenuItemDto>(OnMenuItemTapped);

            MenuItems = new ObservableCollection<MainMenuItemDto>();
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new MainMenuItemDto
            {
                MenuText = MenuConstants.LogOut,
                ViewModelToLoad = typeof(LoginViewModel),
                MenuItemType = MenuItemType.Logout,
            });
        }

        public IAsyncCommand<MainMenuItemDto> MenuItemTappedCommand { get; set; }

        private async Task OnMenuItemTapped(MainMenuItemDto menuItem)
        {
            if (menuItem != null)
            {
                switch (menuItem.MenuText)
                {
                    case MenuConstants.LogOut:
                        _settingsService.Token = null;
                        await _navigationService.ClearBackStack();
                        break;
                    default:
                        break;
                }
                await _navigationService.NavigateToAsync(menuItem.ViewModelToLoad);
            }
        }
    }
}