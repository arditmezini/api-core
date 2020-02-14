using AspNetCoreApi.Models.Common.Emails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);
        Task<List<EmailMessage>> ReceiveEmail(int maxCount = 10);
    }
}