using buzzerApi.Repository.Abstraction;
using buzzerApi.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace buzzerApi.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<Models.User> _passwordHasher;

        public UserService(IUserRepository repository, IPasswordHasher<Models.User> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Models.User> CreateUserAsync(Models.User user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            await _repository.CreateAsync(user);
            return user;
        }

        public async Task<Models.User> GetUserAsync(string mail)
        {
            var user = await _repository.GetAsync(mail);
            if(user == null)
            {
                throw (new Exception("Unknown user"));
            }
            return user;
        }
    }
}
