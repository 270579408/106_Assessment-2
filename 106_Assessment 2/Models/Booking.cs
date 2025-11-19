using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _106_Assessment_2.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("eventId")]
        public string EventId { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("bookingName")]
        public string BookingName { get; set; }

        [BsonElement("bookingEmail")]
        public string BookingEmail { get; set; }

        [BsonElement("bookingPhoneNumber")]
        public string BookingPhoneNumber { get; set; }

        [BsonElement("bookingSpecialReq")]
        public string BookingSpecialReq { get; set; }

        [BsonElement("ticketCount")]
        public int TicketCount { get; set; }

        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; set; }

        [BsonElement("bookingDate")]
        public DateTime BookingDate { get; set; }

    }
}
