﻿using System.Threading.Tasks;

namespace DimitriSauvageTools.Infrastructure.Abstraction
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);
    }
}
