using BookStore.Models.Enum;
using System;
using Xamarin.Forms;

namespace BookStore.Models.Dto
{
    public class MainMenuItemDto : BindableObject
    {
        private string _menuText;
        private MenuItemType _menuItemType;
        private Type _viewModelToLoad;

        public MenuItemType MenuItemType
        {
            get => _menuItemType;
            set
            {
                _menuItemType = value;
                OnPropertyChanged();
            }
        }

        public string MenuText
        {
            get => _menuText;
            set
            {
                _menuText = value;
                OnPropertyChanged();
            }
        }

        public Type ViewModelToLoad
        {
            get => _viewModelToLoad;
            set
            {
                _viewModelToLoad = value;
                OnPropertyChanged();
            }
        }
    }
}