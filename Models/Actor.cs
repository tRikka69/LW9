using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AmateurTheaterMongo.Models
{
    public class Actor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("full_name")]
        public string FullName { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("age")]
        public int Age { get; set; }
    }
}