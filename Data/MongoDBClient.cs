using MongoDB.Driver;

namespace AmateurTheaterMongo.Data
{
    public class MongoDBClient
    {
        private static IMongoDatabase? _db;
        private static MongoDBClient? _instance;

        public static MongoDBClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MongoDBClient();
                }
                return _instance;
            }
        }

        private MongoDBClient()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase("AmateurTheaterDB");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db!.GetCollection<T>(name);
        }
    }
}