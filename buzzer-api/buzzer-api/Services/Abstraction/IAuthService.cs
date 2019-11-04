using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IAuthService
    {
        bool IsTokenValid(string token);

        string GenerateToken(User user);
    }
}
