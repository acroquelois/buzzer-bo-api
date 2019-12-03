using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Options;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IAuthService
    {
        Task<(AuthErrors, UserToken)> LoginAsync(Models.User user);
        bool IsTokenValid(string token);
        UserToken GenerateToken(Models.User user);
    }
}
