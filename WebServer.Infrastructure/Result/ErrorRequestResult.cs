using WebServer.Domain.Enums;
using WebServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Infrastructure.Result
{
    public class ErrorRequestResult : RequestResult
    {
        public ErrorRequestResult(ErrorCode errorCode) : base(false, errorCode)
        {
        }

        public ErrorRequestResult() : base(false)
        {
        }
    }
}
