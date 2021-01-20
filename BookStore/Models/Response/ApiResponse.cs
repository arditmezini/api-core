namespace BookStore.Models.Response
{
    public class ApiResponse<T>
    {
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public object ResponseException { get; set; }
        public T Result { get; set; }
    }
}