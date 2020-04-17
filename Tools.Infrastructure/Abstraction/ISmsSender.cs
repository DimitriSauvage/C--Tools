using System.Threading.Tasks;

namespace Tools.Infrastructure.Abstraction
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
