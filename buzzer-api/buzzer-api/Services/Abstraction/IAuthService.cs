using buzzerApi.Dto;
using buzzerApi.Enum;
using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
