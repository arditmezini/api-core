using AspNetCoreApi.Models.Common.Emails;
using System.Collections.Generic;

namespace AspNetCoreApi.Common.Mail
{
    public static class MailHelper
    {
        public static EmailMessage BuildMail(MailTypeEnum type, List<EmailAddress> from = null, List<EmailAddress> to = null,
            string subject = null, string content = null)
        {
            var emailMsg = new EmailMessage();

            if (from != null)
                emailMsg.From.AddRange(from);
            if (to != null)
                emailMsg.To.AddRange(to);

            switch (type)
            {
                case MailTypeEnum.NewUser:
                    emailMsg.From.Add(new EmailAddress { Name = "", Address = "" });
                    emailMsg.Subject = subject ?? "";
                    emailMsg.Content = content ?? "";
                    break;
                case MailTypeEnum.LoginUser:
                    emailMsg.From.Add(new EmailAddress { Name = "", Address = "" });
                    emailMsg.Subject = subject ?? "";
                    emailMsg.Content = content ?? "";
                    break;
                default:
                    break;
            }

            return emailMsg;
        }
    }
}