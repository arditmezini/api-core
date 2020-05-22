namespace BookStore.Constants
{
    public class ApiConstants
    {
        public const string BaseUrl = "https://localhost:44325/api";

        #region Account
        public const string BaseAccount = BaseUrl + "/account";

        public const string AccountLogin = BaseAccount + "/login";
        public const string AccountRegister = BaseAccount + "/register";
        public const string AccountValidateToken = BaseAccount + "/validate-token?token={token}";
        #endregion
    }
}