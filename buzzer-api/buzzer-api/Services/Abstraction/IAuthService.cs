using buzzerApi.Dto;
using buzzerApi.Enum;
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
