using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Services
{
    public interface IPlayService
    {
        Task<List<Play>> GetAllAsync();
        Task<Play?> GetByIdAsync(string id);
        Task CreateAsync(Play play);
        Task<bool> UpdateAsync(string id, Play play);
        Task<bool> DeleteAsync(string id);
    }
}