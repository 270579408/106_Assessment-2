using MongoDB.Driver;
using _106_Assessment_2.Data;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.ViewModels
{
    public class VisitorViewModel
    {
        private readonly IMongoCollection<Visitor> _visitorCollection;

        public VisitorViewModel()
        {
            var dbHelper = new MongoDBHelper();
            _visitorCollection = dbHelper.GetCollection<Visitor>("Visitors");
        }

        public void AddVisitor(Visitor visitor)
        {
            _visitorCollection.InsertOne(visitor);
        }

        public List<Visitor> GetAllVisitors()
        {
            return _visitorCollection.Find(_ => true).ToList();
        }
    }
}