using MongoDB.Driver;
using _106_Assessment_2.Data;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.ViewModels
{
    public class BookingViewModel
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingViewModel()
        {
            var dbHelper = new MongoDBHelper();
            _bookings = dbHelper.GetCollection<Booking>("Bookings");
        }

        public void AddBooking(Booking e)
        {
            _bookings.InsertOne(e);
        }

        public List<Booking> GetAllBooking()
        {
            return _bookings.Find(_ => true).ToList();
        }
    }
}