using Newtonsoft.Json;

namespace AspNetCoreApi.Models.Common.Paging
{
    public class ApiResponsePagination
    {
        public string Version { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StatusCode { get; set; }

        public string Message { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsError { get; set; }

        public object ResponseException { get; set; }

        public object Result { get; set; }

        public Pagination Pagination { get; set; }

        [JsonConstructor]
        public ApiResponsePagination(string message, object result = null, Pagination pagination = null, int statusCode = 200, string apiVersion = "1.0.0.0")
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
            Version = apiVersion;
            Pagination = pagination;
        }
        public ApiResponsePagination(object result, int statusCode = 200)
        {
            StatusCode = statusCode;
            Result = result;
        }

        public ApiResponsePagination(int statusCode, object apiError)
        {
            StatusCode = statusCode;
            ResponseException = apiError;
            IsError = true;
        }

        public ApiResponsePagination() { }
    }
}