using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Infrastructure.CustomExceptionFilter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public ExceptionFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            // Exception handling is done by ExceptionMiddleware
        }
    }
}
