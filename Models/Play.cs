using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AmateurTheaterMongo.Models
{
    public class Play
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("genre")]
        public Genre Genre { get; set; }

        [BsonElement("theater_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TheaterId { get; set; } = string.Empty;
    }
}