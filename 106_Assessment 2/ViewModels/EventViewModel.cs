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
    }
}