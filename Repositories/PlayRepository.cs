using AmateurTheaterMongo.Data;
using AmateurTheaterMongo.Models;
using MongoDB.Driver;

namespace AmateurTheaterMongo.Repositories
{
    public class PlayRepository : IPlayRepository
    {
        private readonly IMongoCollection<Play> _collection;

        public PlayRepository()
        {
            _collection = MongoDBClient.Instance.GetCollection<Play>("plays");
        }

        public async Task<List<Play>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();
        public async Task<Play?> GetByIdAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Play play) => await _collection.InsertOneAsync(play);
        public async Task UpdateAsync(Play play) => await _collection.ReplaceOneAsync(x => x.Id == play.Id, play);
        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
    }
}