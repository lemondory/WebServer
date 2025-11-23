using System.Net.WebSockets;

namespace WebServer.Infrastructure.WebSockets.Handler
{
    public interface IWebSocketHandler
    {
        Task HandleMessageAsync(WebSocket socket, string message);
    }
}
