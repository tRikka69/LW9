using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}