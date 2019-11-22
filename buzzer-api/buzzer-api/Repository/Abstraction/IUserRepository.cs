using buzzerApi.Models;
using System.Threading.Tasks;

namespace buzzerApi.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);

        Task<User> GetAsync(string mail);
    }
}
