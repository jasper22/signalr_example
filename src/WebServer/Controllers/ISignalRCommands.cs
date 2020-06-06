using System.Threading.Tasks;

namespace WebServer.Controllers
{
    public interface ISignalRCommands
    {
        Task NotifyAll(string param, string message);
    }
}
