using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Infrastructure.Extensions
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException(string message, Exception inner)
            : base(message, inner) { }
    }
}
