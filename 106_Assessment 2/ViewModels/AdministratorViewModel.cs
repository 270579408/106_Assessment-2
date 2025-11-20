using MongoDB.Driver;
using _106_Assessment_2.Data;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.ViewModels
{
    public class AdministratorViewModel
    {
        private readonly IMongoCollection<Administrator> _admin;

        public AdministratorViewModel()
        {
            var dbHelper = new MongoDBHelper();
            _admin = dbHelper.GetCollection<Administrator>("Admin");
        }

        public Administrator GetUserByEmail(string email)
        {
            return _admin.Find(u => u.Email == email).FirstOrDefault();
        }

    }
}