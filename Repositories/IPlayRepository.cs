using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Repositories
{
    public interface IPlayRepository
    {
        Task<List<Play>> GetAllAsync();
        Task<Play?> GetByIdAsync(string id);
        Task CreateAsync(Play play);
        Task UpdateAsync(Play play);
        Task DeleteAsync(string id);
    }
}