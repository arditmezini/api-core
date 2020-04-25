namespace AspNetCoreApi.Models.Common.Configurations
{
    public class JwtConfig
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtExpireDays { get; set; }
    }
}