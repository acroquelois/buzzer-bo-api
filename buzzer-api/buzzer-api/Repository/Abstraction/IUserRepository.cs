using buzzerApi.Models;
using System.Threading.Tasks;

namespace buzzerApi.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);

        User GetAsync(string mail);
    }
}
