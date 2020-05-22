﻿using BookStore.Contracts.Services.General;
using BookStore.ViewModels.Base;

namespace BookStore.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(INavigationService navigationService)
            : base(navigationService)
        { }
    }
}