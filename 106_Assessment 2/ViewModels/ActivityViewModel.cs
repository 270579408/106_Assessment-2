using MongoDB.Driver;
using _106_Assessment_2.Data;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.ViewModels
{
    public class ActivityViewModel
    {
        private readonly IMongoCollection<Activity> _activities;

        public ActivityViewModel()
        {
            var dbHelper = new MongoDBHelper();
            _activities = dbHelper.GetCollection<Activity>("Activities");
        }

        public void AddActivity(Activity e)
        {
            _activities.InsertOne(e);
        }

        public List<Activity> GetAllActivities()
        {
            return _activities.Find(_ => true).ToList();
        }
    }
}