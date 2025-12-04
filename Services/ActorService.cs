using AmateurTheaterMongo.Models;
using AmateurTheaterMongo.Repositories;

namespace AmateurTheaterMongo.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _repository;
        public ActorService(IActorRepository repository) => _repository = repository;

        public async Task<List<Actor>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Actor?> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);
        public async Task CreateAsync(Actor actor) => await _repository.CreateAsync(actor);

        public async Task<bool> UpdateAsync(string id, Actor updatedActor)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            updatedActor.Id = id;
            await _repository.UpdateAsync(updatedActor);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}