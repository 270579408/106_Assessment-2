using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace _106_Assessment_2.Models
{
    public class Event
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

        [BsonElement("eventDate")]
        public DateTime EventDate { get; set; }

        [BsonElement("price")]
        public string Price { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; } = new(); 

        [BsonElement("featured")]
        public bool Featured { get; set; }

        [BsonElement("registeredUserIds")]
        public List<string> RegisteredUserIds { get; set; } = new();

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

    }
}
