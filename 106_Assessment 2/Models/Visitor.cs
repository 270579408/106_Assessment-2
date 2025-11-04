using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _106_Assessment_2.Models
{
    public class Visitor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("interest")]
        public string Interest { get; set; }
    }
}