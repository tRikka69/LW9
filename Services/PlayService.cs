using AmateurTheaterMongo.Models;
using AmateurTheaterMongo.Repositories;

namespace AmateurTheaterMongo.Services
{
    public class PlayService : IPlayService
    {
        private readonly IPlayRepository _repository;
        public PlayService(IPlayRepository repository) => _repository = repository;

        public async Task<List<Play>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Play?> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);
        public async Task CreateAsync(Play play) => await _repository.CreateAsync(play);

        public async Task<bool> UpdateAsync(string id, Play updatedPlay)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            updatedPlay.Id = id;
            await _repository.UpdateAsync(updatedPlay);
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