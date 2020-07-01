using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using DimitriSauvageTools.Mail.Exceptions;
using DimitriSauvageTools.Mail.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace DimitriSauvageTools.Mail.Senders
{
    public class MailSender
    {
        #region Fields

        /// <summary>
        /// Configuration for the mail sending
        /// </summary>
        private MailConfiguration MailConfiguration { get; }

        #endregion

        #region Constructor

        public MailSender([NotNull] MailConfiguration mailConfig)
        {
            if (mailConfig == null) throw new ArgumentNullException(nameof(mailConfig));
            try
            {
                Validator.ValidateObject(mailConfig, new ValidationContext(mailConfig), true);
            }
            catch (ValidationException)
            {
                throw new EmailConfigurationException("Email configuration is not correct");
            }

            this.MailConfiguration = mailConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Send an email
        /// </summary>
        /// <returns>Sending result</returns>
        public async Task<MailSendingResult> SendAsync(MailMetadata mailMetadata)
        {
            //Create the message
            return await this.SendAsync(this.CreateMessage(mailMetadata));
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Create the message to send
        /// </summary>
        /// <param name="mailMetadata">Mail metadata</param>
        /// <returns>Message to send</returns>
        /// <exception cref="FileNotFoundException">An attachment does not exist</exception>
        private MimeMessage CreateMessage(MailMetadata mailMetadata)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(
                new MailboxAddress(
                    this.MailConfiguration.SendingDisplayName,
                    this.MailConfiguration.SendingEmail));
            mailMessage.To.AddRange(mailMetadata.MainReceivers);
            mailMessage.Cc.AddRange(mailMetadata.SecondaryReceivers);
            mailMessage.Bcc.AddRange(mailMetadata.HiddenReceivers);
            mailMessage.Subject = mailMetadata.MailSubject;

            //Create the body
            var bodyBuilder = new BodyBuilder();
            if (mailMetadata.ContentFormat == TextFormat.Text)
                bodyBuilder.TextBody = mailMetadata.MailContent;
            else
                bodyBuilder.HtmlBody = mailMetadata.MailContent;

            //Add attachments
            foreach (var attachment in mailMetadata.Attachments)
            {
                if (!attachment.Exists) throw new FileNotFoundException($"The file {attachment.Name} does not exist");
                bodyBuilder
                    .Attachments
                    .Add(attachment.Name, attachment.OpenRead());
            }

            mailMessage.Body = bodyBuilder.ToMessageBody();
            mailMessage.Date = new DateTimeOffset();
            mailMessage.Importance = mailMetadata.MailImportance;
            mailMessage.Priority = mailMetadata.MailPriority;

            return mailMessage;
        }

        /// <summary>
        /// Send the mail
        /// </summary>
        /// <param name="mailMessage">Mail to send</param>
        /// <returns>Sending result</returns>
        private async Task<MailSendingResult> SendAsync(MimeMessage mailMessage)
        {
            var result = new MailSendingResult();
            using var client = new SmtpClient();
            try
            {
                //Connect to the SMTP Server
                await client.ConnectAsync(this.MailConfiguration.SmtpServer, this.MailConfiguration.Port, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                //Authentication
                await client.AuthenticateAsync(this.MailConfiguration.UserName, this.MailConfiguration.Password);

                //Send the mail
                await client.SendAsync(mailMessage);
            }
            catch (Exception e)
            {
                result.Sent = false;
                result.ErrorMessage = $"Error while sending the mail : {e.Message}";
            }
            finally
            {
                await client.DisconnectAsync(true);
            }

            return result;
        }

        #endregion
    }
}