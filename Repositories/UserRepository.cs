using AmateurTheaterMongo.Data;
using AmateurTheaterMongo.Models;
using MongoDB.Driver;

namespace AmateurTheaterMongo.Repositories 
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository()
        {
            _collection = MongoDBClient.Instance.GetCollection<User>("users");
        }

        public async Task CreateAsync(User user) => 
            await _collection.InsertOneAsync(user);

        public async Task<User?> GetByEmailAsync(string email) => 
            await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
    }
}