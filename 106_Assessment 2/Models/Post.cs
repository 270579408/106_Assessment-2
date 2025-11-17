using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _106_Assessment_2.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("uploaderId")]
        public string UploaderId { get; set; }

        [BsonElement("reactorId")]
        public List<string> ReactorId { get; set; }

        [BsonElement("postedDate")]
        public DateTime PostedDate { get; set; }
    }
}
