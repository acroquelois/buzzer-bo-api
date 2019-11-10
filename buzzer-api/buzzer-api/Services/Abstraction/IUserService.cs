using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IUserService
    {
        Task<Models.User> CreateUserAsync(Models.User user);

        Task<Models.User> GetUserAsync(string mail);
    }
}
