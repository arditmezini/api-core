using AspNetCoreApi.Service.Contracts;
using Hangfire;
using System;
using System.Linq.Expressions;

namespace AspNetCoreApi.Service.Services
{
    public class HangfireJobService : IHangfireJobService
    {
        public string ProcessDelayedJob<T>(Expression<Action<T>> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        public string ProcessFireAndForgetJobs<T>(Expression<Action<T>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        public void ProcessRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default")
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone, queue);
        }
    }
}
