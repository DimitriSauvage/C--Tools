using System.ComponentModel.DataAnnotations;

namespace DimitriSauvageTools.Mail.Models
{
    public class MailConfiguration
    {
        /// <summary>
        /// Email with which send the mail
        /// </summary>
        [Required]
        public string SendingEmail { get; set; }

        /// <summary>
        /// Display name where sending email
        /// </summary>
        public string SendingDisplayName { get; set; }

        /// <summary>
        /// SMTP Server to use to send the mail
        /// </summary>
        [Required]
        public string SmtpServer { get; set; }

        /// <summary>
        /// SMTP Port to use to send the mail
        /// </summary>
        public int Port { get; set; } = 587;

        /// <summary>
        /// Username or email address to connect with
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password for the username
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}