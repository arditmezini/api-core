using AspNetCoreApi.Models.Common.Emails;
using AspNetCoreApi.Service.Contracts;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _config;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _config = emailConfiguration;
        }

        public async Task<List<EmailMessage>> ReceiveEmail(int maxCount = 10)
        {
            if (_config.MailServiceActive)
            {
                using var pop3Client = new Pop3Client();
                await pop3Client.ConnectAsync(_config.PopServer, _config.PopPort, _config.EnableSsl);
                await pop3Client.AuthenticateAsync(_config.PopUsername, _config.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < pop3Client.Count && i < maxCount; i++)
                {
                    var message = await pop3Client.GetMessageAsync(i);
                    var emailMessage = new EmailMessage
                    {
                        Body = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.To.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.From.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emails.Add(emailMessage);
                }
                return emails;
            }
            else
                return new List<EmailMessage>();
        }

        public async Task Send(EmailMessage emailMessage)
        {
            if (_config.MailServiceActive)
            {
                var message = new MimeMessage();
                message.To.AddRange(emailMessage.To.Select(x => new MailboxAddress(x.Name, x.Address)));
                message.From.AddRange(emailMessage.From.Select(x => new MailboxAddress(x.Name, x.Address)));
                message.Subject = emailMessage.Subject;
                message.Body = new BodyBuilder
                {
                    HtmlBody = emailMessage.Body
                }.ToMessageBody(); ;

                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_config.SmtpServer, _config.SmtpPort, _config.EnableSsl);
                await smtpClient.AuthenticateAsync(_config.SmtpUsername, _config.SmtpPassword);
                await smtpClient.SendAsync(message).ConfigureAwait(false);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}