using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Repositories
{
    public interface ITheaterRepository
    {
        Task<List<Theater>> GetAllAsync();
        Task<Theater?> GetByIdAsync(string id);
        Task CreateAsync(Theater theater);
        Task UpdateAsync(Theater theater);
        Task DeleteAsync(string id);
    }
}