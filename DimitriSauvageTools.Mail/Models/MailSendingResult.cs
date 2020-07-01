using System;

namespace DimitriSauvageTools.Mail.Models
{
    public class MailSendingResult
    {
        /// <summary>
        /// If the mail has been sent
        /// </summary>
        public bool Sent { get; set; } = true;

        /// <summary>
        /// Message of the error
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}