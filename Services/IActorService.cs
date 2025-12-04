using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Services
{
    public interface IActorService
    {
        Task<List<Actor>> GetAllAsync();
        Task<Actor?> GetByIdAsync(string id);
        Task CreateAsync(Actor actor);
        Task<bool> UpdateAsync(string id, Actor actor);
        Task<bool> DeleteAsync(string id);
    }
}