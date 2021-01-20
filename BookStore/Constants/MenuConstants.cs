using BookStore.Models.Dto;
using BookStore.Models.Enum;
using BookStore.ViewModels;

namespace BookStore.Constants
{
    public class MenuConstants
    {
        public const string Profile = "Profile";
        public static MainMenuItemDto ItemProfile = new MainMenuItemDto
        {
            MenuText = Profile,
            ViewModelToLoad = typeof(ProfileViewModel),
            MenuItemType = MenuItemType.Profile
        };

        public const string Authors = "Authors";
        public static MainMenuItemDto ItemAuthors = new MainMenuItemDto
        {
            MenuText = Authors,
            ViewModelToLoad = typeof(AuthorViewModel),
            MenuItemType = MenuItemType.Authors
        };

        public const string News = "News";
        public static MainMenuItemDto ItemNews = new MainMenuItemDto
        {
            MenuText = News,
            ViewModelToLoad = typeof(NewsViewModel),
            MenuItemType = MenuItemType.News
        };

        public const string LogOut = "Log out";
        public static MainMenuItemDto ItemLogOut = new MainMenuItemDto
        {
            MenuText = LogOut,
            ViewModelToLoad = typeof(LoginViewModel),
            MenuItemType = MenuItemType.Logout,
        };
    }
}