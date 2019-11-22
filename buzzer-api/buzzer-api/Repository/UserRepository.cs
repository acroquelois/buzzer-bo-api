using buzzerApi.Models;
using buzzerApi.Repository.Abstraction;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace buzzerApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BuzzerApiContext db;

        public UserRepository(BuzzerApiContext dbContext)
        {
            db = dbContext;
        }

        public async Task CreateAsync(User user)
        {
            db.User.Add(user);
            await db.SaveChangesAsync();
        }

        public async Task<User> GetAsync(string mail)
        {
            var ret = await db.User
                .Where(x => x.Email == mail)
                .FirstOrDefaultAsync();
            return ret;
        }
    }
}
