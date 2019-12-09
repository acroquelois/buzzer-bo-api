using System.Threading.Tasks;

namespace buzzerApi.Services.Abstraction
{
    public interface IUserService
    {
        Task<Models.User> CreateUserAsync(Models.User user);

        Models.User GetUserAsync(string mail);
    }
}
