using Xamarin.Essentials;

namespace BookStore.Constants
{
    public class ApiConstants
    {
        public static string BaseUrl = DeviceInfo.Platform.Equals(DevicePlatform.Android)
            ? "https://10.0.2.2:44325/api/1.0/" : "https://localhost:44325/api/1.0/";
        public const string JwtAuthHeader = "authorization";
        public const string JwtBarer = "Bearer ";
        public const string JsonMediaType = "application/json";

        #region Account
        public const string BaseAccount = "account";

        public const string AccountLogin = BaseAccount + "/login";
        public const string AccountRegister = BaseAccount + "/register";
        public const string AccountValidateToken = BaseAccount + "/validate";
        public const string AccountProfile = BaseAccount + "/profile/{email}";
        #endregion

        #region General Data
        public const string BaseData = "data";

        public const string DataRoles = BaseData + "/roles";
        #endregion

        #region Statistics
        public const string BaseStatistics = "statistics";

        public const string StatisticsDashboard = BaseStatistics + "/dashboard";
        #endregion

        #region Author
        public const string BaseAuthor = "author";
        #endregion
    }
}