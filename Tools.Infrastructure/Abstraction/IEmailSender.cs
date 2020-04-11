using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Infrastructure.Abstraction
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);
    }
}
