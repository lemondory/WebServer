using WebServer.Domain.Entities;
using WebServer.Shared.Models;

namespace WebServer.Contracts.Services
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(AccountEntity account);
    }
}
