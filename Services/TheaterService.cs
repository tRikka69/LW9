using AmateurTheaterMongo.Models;
using AmateurTheaterMongo.Repositories;

namespace AmateurTheaterMongo.Services
{
    public class TheaterService : ITheaterService
    {
        private readonly ITheaterRepository _repository;

        public TheaterService(ITheaterRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Theater>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Theater?> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);
        public async Task CreateAsync(Theater theater) => await _repository.CreateAsync(theater);

        public async Task<bool> UpdateAsync(string id, Theater updatedTheater)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            updatedTheater.Id = id; 
            await _repository.UpdateAsync(updatedTheater);
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