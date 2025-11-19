using MongoDB.Driver;
using _106_Assessment_2.Data;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.ViewModels
{
    public class EventViewModel
    {
        private readonly IMongoCollection<Event> _events;

        public EventViewModel()
        {
            var dbHelper = new MongoDBHelper();
            _events = dbHelper.GetCollection<Event>("Events");
        }

        public void AddEvent(Event e)
        {
            _events.InsertOne(e);
        }

        public List<Event> GetAllEvents()
        {
            return _events.Find(_ => true).ToList();
        }

        public Event GetEventById(string id)
        {
            return _events.Find(u => u.Id == id).FirstOrDefault();
        }

        public void UpdateEvent(Event ev)
        {
            if (string.IsNullOrEmpty(ev.Id))
                throw new ArgumentException("Event ID cannot be null or empty.");

            var filter = Builders<Event>.Filter.Eq(e => e.Id, ev.Id);
            var update = Builders<Event>.Update
                .Set(e => e.Title, ev.Title)
                .Set(e => e.Description, ev.Description)
                .Set(e => e.EventDate, ev.EventDate)
                .Set(e => e.Price, ev.Price)
                .Set(e => e.Tags, ev.Tags)
                .Set(e => e.Featured, ev.Featured)
                .Set(e => e.ImageUrl, ev.ImageUrl)
                .Set(e => e.RegisteredUserIds, ev.RegisteredUserIds)
                .Set(e => e.CreatedAt, ev.CreatedAt);

            _events.UpdateOne(filter, update);
        }

        public void DeleteEvent(string eventId)
        {
            if (string.IsNullOrEmpty(eventId))
                throw new ArgumentException("Event ID cannot be null or empty.");

            var filter = Builders<Event>.Filter.Eq(e => e.Id, eventId);
            _events.DeleteOne(filter);
        }


    }
}