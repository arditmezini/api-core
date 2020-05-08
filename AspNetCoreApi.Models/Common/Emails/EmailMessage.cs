using System.Collections.Generic;

namespace AspNetCoreApi.Models.Common.Emails
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            To = new List<EmailAddress>();
            From = new List<EmailAddress>();
        }

        public List<EmailAddress> To { get; set; }
        public List<EmailAddress> From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}