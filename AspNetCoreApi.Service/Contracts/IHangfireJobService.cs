using System;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IHangfireJobService
    {
        Task ProcessFireAndForgetJobs<T>(T content);
        void ProcessDelayedJob<T>(T content, TimeSpan delay);
        void ProcessRecurringJob<T>(string recurringJobId, T content, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default");
    }
}