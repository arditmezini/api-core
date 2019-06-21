namespace AspNetCoreApi.Models.Common
{
    public class JwtConfig
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtExpireDays { get; set; }
    }
}