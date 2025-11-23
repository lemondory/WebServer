using WebServer.Domain.Entities;
using WebServer.Persistance.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Infrastructure.LogEntries
{
    public class LogFilter : IActionFilter
    {
        private LogEntity? _logEntity;

        public LogFilter()
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity?.IsAuthenticated == true)
            {
                _logEntity = new LogEntity
                {
                    id = 0,
                    username = context.HttpContext.User.Identity.Name ?? "Guest",
                    email = context.HttpContext.User.Claims.Count() > 1 ? context.HttpContext.User.Claims.ElementAt(1).Value : "-",
                    ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                    created_at = DateTime.Now,
                    controller = context.ActionDescriptor.DisplayName ?? "-",
                    parameters = context.ActionArguments.Aggregate("", (current, p) => current + p.Key + ": " + (p.Value?.ToString() ?? "null") + Environment.NewLine)
                };
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User;
            var isAuthenticated = user.Identity?.IsAuthenticated ?? false;

            // 예외 발생 여부 확인
            if (context.Exception != null)
            {
                // 예외 정보 로깅
                var exception = context.Exception;
                Console.WriteLine($"[Exception] {exception.GetType().Name}: {exception.Message}");

                // Exception logging is handled by ExceptionMiddleware
                context.ExceptionHandled = true;
            }
            else if (isAuthenticated && _logEntity != null)
            {
                // Log successful request
                _logEntity.status = httpContext.Response.StatusCode.ToString();
                _logEntity.description = "-";
                _logEntity.type = httpContext.Response.StatusCode == 200 ? EnumLogType.Log.ToString() : EnumLogType.Warning.ToString();
            }
        }
    }
}
