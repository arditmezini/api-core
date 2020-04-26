using System;
using System.Collections.Generic;

namespace AspNetCoreApi.Models.Common
{
    public class HealthCheckDto
    {
        public HealthCheckSummary HealthCheckSummary { get; set; }
        public IEnumerable<HealthCheckService> HealthCheckServices { get; set; }
    }

    public class HealthCheckSummary : BaseHealthCheck
    { }

    public class HealthCheckService : BaseHealthCheck
    {
        public string Service { get; set; }
        public string Tags { get; set; }
    }

    public class BaseHealthCheck
    {
        public string Status { get; set; }
        public TimeSpan Duration { get; set; }
    }
}