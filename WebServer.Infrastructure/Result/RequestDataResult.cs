using WebServer.Contracts.Infrastructure;
using WebServer.Shared.Enums;

namespace WebServer.Infrastructure.Result
{
    public class RequestDataResult<T> : RequestResult, IRequestDataResult<T>
    {
        public RequestDataResult(T data, bool success, ErrorCode errorCode) : base(success, errorCode)
        {
            Data = data;
        }

        public RequestDataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
        public new ErrorCode ErrorCode => base.ErrorCode;
    }
}
