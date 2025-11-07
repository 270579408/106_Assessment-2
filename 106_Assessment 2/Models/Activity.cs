using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _106_Assessment_2.Models
{
    public class Activity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; } = new();

    }
}
