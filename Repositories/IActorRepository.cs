using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Repositories
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetAllAsync();
        Task<Actor?> GetByIdAsync(string id);
        Task CreateAsync(Actor actor);
        Task UpdateAsync(Actor actor);
        Task DeleteAsync(string id);
    }
}