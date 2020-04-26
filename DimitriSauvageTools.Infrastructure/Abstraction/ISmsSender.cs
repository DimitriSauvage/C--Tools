using System.Threading.Tasks;

namespace DimitriSauvageTools.Infrastructure.Abstraction
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
