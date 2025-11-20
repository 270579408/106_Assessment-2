using _106_Assessment_2.Data;
using _106_Assessment_2.Models;
using _106_Assessment_2.View.Pages;
using MongoDB.Driver;

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

        public List<Booking> GetBookingById(string id)
        {
            return _bookings.Find(u => u.UserId == id).ToList();
        }

        public void DeleteBooking(string bookingId)
        {
            if (string.IsNullOrEmpty(bookingId))
                throw new ArgumentException("Booking ID cannot be null or empty.");

            var filter = Builders<Booking>.Filter.Eq(b => b.Id, bookingId);
            _bookings.DeleteOne(filter);
        }

    }
}