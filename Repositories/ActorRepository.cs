using AmateurTheaterMongo.Data;
using AmateurTheaterMongo.Models;
using MongoDB.Driver;

namespace AmateurTheaterMongo.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly IMongoCollection<Actor> _collection;

        public ActorRepository()
        {
            _collection = MongoDBClient.Instance.GetCollection<Actor>("actors");
        }

        public async Task<List<Actor>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();
        public async Task<Actor?> GetByIdAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Actor actor) => await _collection.InsertOneAsync(actor);
        public async Task UpdateAsync(Actor actor) => await _collection.ReplaceOneAsync(x => x.Id == actor.Id, actor);
        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
    }
}