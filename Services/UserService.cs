using AmateurTheaterMongo.Models;
using AmateurTheaterMongo.Repositories;

namespace AmateurTheaterMongo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterUserAsync(User user) => await _repository.CreateAsync(user);
        public async Task<User?> FindUserAsync(string email) => await _repository.GetByEmailAsync(email);
    }
}