using buzzerApi.Dto;
using buzzerApi.Enum;

namespace buzzerApi.Services.Abstraction
{
    public interface IAuthService
    {
        (AuthErrors, UserToken) LoginAsync(Models.User user);

        bool IsTokenValid(string token);
        UserToken GenerateToken(Models.User user);
    }
}
