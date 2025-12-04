using AmateurTheaterMongo.Data;
using AmateurTheaterMongo.Models;
using MongoDB.Driver;

namespace AmateurTheaterMongo.Repositories
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly IMongoCollection<Theater> _collection;

        public TheaterRepository()
        {
            _collection = MongoDBClient.Instance.GetCollection<Theater>("theaters");
        }

        public async Task<List<Theater>> GetAllAsync() => 
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Theater?> GetByIdAsync(string id) => 
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Theater theater) => 
            await _collection.InsertOneAsync(theater);

        public async Task UpdateAsync(Theater theater) => 
            await _collection.ReplaceOneAsync(x => x.Id == theater.Id, theater);

        public async Task DeleteAsync(string id) => 
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}