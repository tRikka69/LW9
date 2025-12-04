using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user);
        Task<User?> FindUserAsync(string email);
    }
}