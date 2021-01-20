using BookStore.Models.Dto;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BookStore.Converters
{
    public class MenuItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemTappedEventArgs eventArgs)
                return eventArgs.Item as MainMenuItemDto;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}