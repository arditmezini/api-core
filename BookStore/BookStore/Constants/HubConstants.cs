
using Xamarin.Essentials;

namespace BookStore.Constants
{
    public class HubConstants
    {
        #region NewsHub
        public static string NewsHubUrl = DeviceInfo.Platform.Equals(DevicePlatform.Android)
            ? "https://10.0.2.2:44325/newshub" : "https://localhost:44325/newshub";

        public const string OpenNews = "OpenNews";
        public const string SendNews = "SendNews";
        public const string CloseNews = "CloseNews";
        #endregion
    }
}