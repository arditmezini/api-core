namespace AspNetCoreApi.Models.Common.Configurations
{
    public class HealthChecksConfig
    {
        public string Url { get; set; }
        public Drive Drive { get; set; }
    }

    public class Drive
    {
        public string Letter { get; set; }
        public long MinSpace { get; set; }
    }
}