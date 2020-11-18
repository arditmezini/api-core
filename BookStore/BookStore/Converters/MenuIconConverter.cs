using BookStore.Constants;
using BookStore.Models.Enum;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BookStore.Converters
{
    public class MenuIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (MenuItemType)value;

            switch (type)
            {
                case MenuItemType.Authors:
                    return IconConstants.Authors;
                case MenuItemType.Profile:
                    return IconConstants.Profile;
                case MenuItemType.Logout:
                    return IconConstants.Logout;
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}