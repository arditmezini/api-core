using System.Net.Mail;

namespace BookStore.Utility.Validations
{
    public static class FormsValidation
    {
        public static string IsEmptyString(this string property)
        {
            return string.IsNullOrWhiteSpace(property) ?
                string.Empty : $"{nameof(property)} is required.";
        }

        public static string IsEmail(this string email)
        {
            var addr = new MailAddress($"{email}");
            return addr.Address == $"{email}" ?
                string.Empty : "Email address is not valid.";
        }
    }
}
