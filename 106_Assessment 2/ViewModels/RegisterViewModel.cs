using MongoDB.Driver;
using _106_Assessment_2.Data;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.ViewModels
{
    public class RegisterViewModel
    {
        private readonly IMongoCollection<User> _user;

        public RegisterViewModel()
        {
            var dbHelper = new MongoDBHelper();
            _user = dbHelper.GetCollection<User>("User");
        }

        public void AddUser(User e)
        {
            _user.InsertOne(e);
        }

        public List<User> GetAllUser()
        {
            return _user.Find(_ => true).ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _user.Find(u => u.Email == email).FirstOrDefault();
        }

        public User GetUserById(string id)
        {
            return _user.Find(u => u.ID == id).FirstOrDefault();
        }
        public void DeleteUser(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.ID, userId);
            _user.DeleteOne(filter);
        }

    }
}