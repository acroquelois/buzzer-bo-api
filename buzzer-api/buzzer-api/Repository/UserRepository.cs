using buzzerApi.Models;
using buzzerApi.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

        public User GetAsync(string mail)
        {
            var ret = db.User
                .Where(x => x.Email == mail)
                .FirstOrDefault();
            return ret;
        }
    }
}
