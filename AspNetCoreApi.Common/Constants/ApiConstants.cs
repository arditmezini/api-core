namespace AspNetCoreApi.Common.Constants
{
    public class ApiConstants
    {
        #region ApiVersion
        public const string Version1 = "1.0";
        public const string Version2 = "2.0";
        #endregion

        #region BaseRoutes
        public const string BaseStatistics = "api/{version:apiVersion}/statistics/[action]";
        public const string BasePublisher = "api/{version:apiVersion}/publisher";
        public const string BaseData = "api/{version:apiVersion}/data/[action]";
        public const string BaseBook = "api/{version:apiVersion}/book";
        public const string BaseBookCategory = "api/{version:apiVersion}/book-category";
        public const string BaseAuthor = "api/{version:apiVersion}/author";
        public const string BaseAccount = "api/{version:apiVersion}/account/[action]";
        #endregion
    }
}