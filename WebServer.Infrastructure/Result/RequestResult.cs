using WebServer.Contracts.Infrastructure;
using WebServer.Shared.Enums;

namespace WebServer.Infrastructure.Result
{
    public class RequestResult : IRequestResult
    {
        public RequestResult(bool success, ErrorCode errorCode) : this(success)
        {
            ErrorCode = errorCode;
        }

        public RequestResult(bool success)
        {
            Success = success;
            ErrorCode = ErrorCode.NONE;
        }
        public bool Success { get; }
        public ErrorCode ErrorCode { get; }
    }
}
