using DimitriSauvageTools.Exceptions;

namespace DimitriSauvageTools.Mail.Exceptions
{
    public class EmailConfigurationException : AppException
    {
        public EmailConfigurationException() : this("Email configuration is not correct or null")
        {
        }

        public EmailConfigurationException(string message) : base(message)
        {
        }
    }
}