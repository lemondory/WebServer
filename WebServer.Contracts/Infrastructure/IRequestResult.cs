using WebServer.Shared.Enums;

namespace WebServer.Contracts.Infrastructure
{
    public interface IRequestResult
    {
        bool Success { get; }
        ErrorCode ErrorCode { get; }
    }
}
