using AspNetCoreApi.Models.Common.Emails;
using AspNetCoreApi.Service.Contracts;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async Task<List<EmailMessage>> ReceiveEmail(int maxCount = 10)
        {
            if (_emailConfiguration.MailServiceActive)
            {
                using (var emailClient = new Pop3Client())
                {
                    await emailClient.ConnectAsync(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    await emailClient.AuthenticateAsync(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                    List<EmailMessage> emails = new List<EmailMessage>();
                    for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                    {
                        var message = await emailClient.GetMessageAsync(i);
                        var emailMessage = new EmailMessage
                        {
                            Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                            Subject = message.Subject
                        };
                        emailMessage.To.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                        emailMessage.From.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                        emails.Add(emailMessage);
                    }

                    return emails;
                }
            }
            else
                return new List<EmailMessage>();
        }

        public async Task Send(EmailMessage emailMessage)
        {
            if (_emailConfiguration.MailServiceActive)
            {
                var message = new MimeMessage();
                message.To.AddRange(emailMessage.To.Select(x => new MailboxAddress(x.Name, x.Address)));
                message.From.AddRange(emailMessage.From.Select(x => new MailboxAddress(x.Name, x.Address)));

                message.Subject = emailMessage.Subject;

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailMessage.Content
                };

                using (var emailClient = new SmtpClient())
                {
                    //The last parameter here is to use SSL (Which you should!)
                    await emailClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                    //Remove any OAuth functionality as we won't be using it. 
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    await emailClient.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                    await emailClient.SendAsync(message).ConfigureAwait(false);

                    await emailClient.DisconnectAsync(true);
                }
            }
        }
    }
}
