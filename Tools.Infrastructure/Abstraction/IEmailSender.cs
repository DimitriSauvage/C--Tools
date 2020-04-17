using System.Threading.Tasks;

namespace Tools.Infrastructure.Abstraction
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);
    }
}
