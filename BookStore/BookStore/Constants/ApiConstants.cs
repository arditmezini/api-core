using Xamarin.Essentials;

namespace BookStore.Constants
{
    public class ApiConstants
    {
        public static string BaseUrl = DeviceInfo.Platform.Equals(DevicePlatform.Android) ? "https://10.0.2.2:44325/api/" : "https://localhost:44325/api/";
        public const string JwtAuthHeader = "Auth";
        public const string JsonMediaType = "application/json";

        #region Account
        public const string BaseAccount = "account";

        public const string AccountLogin = BaseAccount + "/login";
        public static string AccountRegister = BaseAccount + "/register";
        public static string AccountValidateToken = BaseAccount + "/validate-token?token={token}";
        #endregion
    }
}