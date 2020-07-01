using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using MimeKit;
using MimeKit.Text;

namespace DimitriSauvageTools.Mail.Models
{
    public class MailMetadata
    {
        /// <summary>
        /// Main receivers of the email
        /// </summary>
        public IEnumerable<MailboxAddress> MainReceivers { get; set; } = new List<MailboxAddress>();

        /// <summary>
        /// Secondary receivers of the email
        /// </summary>
        public IEnumerable<MailboxAddress> SecondaryReceivers { get; set; } = new List<MailboxAddress>();

        /// <summary>
        /// Hidden receivers of the email
        /// </summary>
        public IEnumerable<MailboxAddress> HiddenReceivers { get; set; } = new List<MailboxAddress>();

        /// <summary>
        /// Subject of the email
        /// </summary>
        public string MailSubject { get; set; }

        /// <summary>
        /// Content of the mail
        /// </summary>
        public string MailContent { get; set; }

        /// <summary>
        /// Format of the mail content
        /// </summary>
        public TextFormat ContentFormat { get; set; } = TextFormat.Html;

        /// <summary>
        /// Attachment files
        /// </summary>
        public IEnumerable<FileInfo> Attachments { get; set; } = new List<FileInfo>();

        /// <summary>
        /// Message importance
        /// </summary>
        public MessageImportance MailImportance { get; set; } = MessageImportance.Normal;

        /// <summary>
        /// Mail priority
        /// </summary>
        public MessagePriority MailPriority { get; set; } = MessagePriority.Normal;
    }
}