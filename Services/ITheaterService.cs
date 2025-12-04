using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Services
{
    public interface ITheaterService
    {
        Task<List<Theater>> GetAllAsync();
        Task<Theater?> GetByIdAsync(string id);
        Task CreateAsync(Theater theater);
        Task<bool> UpdateAsync(string id, Theater theater);
        Task<bool> DeleteAsync(string id);
    }
}