using AspNetCoreApi.Models.Common.Emails;
using System;
using System.IO;
using System.Reflection;

namespace AspNetCoreApi.Common.Mail
{
    public static class MailHelper
    {
        public static EmailMessage BuildMail(MailTypeEnum type, EmailAddress from = null, EmailAddress to = null)
        {
            var emailMsg = new EmailMessage();

            if (from != null)
                emailMsg.From.Add(from);
            if (to != null)
                emailMsg.To.Add(to);

            switch (type)
            {
                case MailTypeEnum.NewUser:
                    emailMsg.Subject = "New Users";
                    emailMsg.Body = GetTemplate(MailTypeEnum.NewUser.ToString()) ?? "";

                    //Replace
                    emailMsg.Body = emailMsg.Body.Replace("@name@", to.Name);

                    break;
                case MailTypeEnum.LoginUser:
                    emailMsg.Subject = "Login User";
                    emailMsg.Body = GetTemplate(MailTypeEnum.LoginUser.ToString()) ?? "";

                    //Replace
                    emailMsg.Body = emailMsg.Body.Replace("@name@", to.Name);
                    emailMsg.Body = emailMsg.Body.Replace("@time@", DateTime.Now.ToString());

                    break;
                default:
                    break;
            }

            return emailMsg;
        }

        public static string GetTemplate(string templateName)
        {
            var dirPath = Path.GetDirectoryName(Path.GetFullPath(Assembly.GetExecutingAssembly().Location));
            var filePath = dirPath + $"\\Mail\\EmailTemplates\\{templateName}.html";
            string htmlTemplate = File.ReadAllText(filePath);
            return htmlTemplate;
        }
    }
}