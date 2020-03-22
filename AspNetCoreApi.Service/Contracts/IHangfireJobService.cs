using System;
using System.Linq.Expressions;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IHangfireJobService
    {
        string ProcessFireAndForgetJobs<T>(Expression<Action<T>> methodCall);
        string ProcessDelayedJob<T>(Expression<Action<T>> methodCall, TimeSpan delay);
        void ProcessRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default");
    }
}