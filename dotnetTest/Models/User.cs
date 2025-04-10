using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnetTest.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("firstName")]
        public string? FirstName { get; set; }
        [BsonElement("lastName")]
        public string? LastName { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }
        
        [BsonElement("occupation")]
        public string Occupation { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
} 