namespace AspNetCoreApi.Models.Common.Emails
{
    public class EmailConfiguration
    {
        //Status
        public bool MailServiceActive { get; set; }

        //Send
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        //Recive
        public string PopServer { get; set; }
        public int PopPort { get; set; }
        public string PopUsername { get; set; }
        public string PopPassword { get; set; }
    }
}