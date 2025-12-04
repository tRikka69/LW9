using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AmateurTheaterMongo.Models
{
    public class Theater
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [Required(ErrorMessage = "Назва обов'язкова")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("city")]
        [Required]
        [RegularExpression(@"^[A-ZА-ЯІЇЄ][a-zа-яіїє]+(?:[\s-][A-ZА-ЯІЇЄ][a-zа-яіїє]+)*$", ErrorMessage = "З великої літери")]
        public string City { get; set; } = string.Empty;
    }
}