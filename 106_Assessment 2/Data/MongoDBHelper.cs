using MongoDB.Driver;
using MongoDB.Bson;

namespace _106_Assessment_2.Data
{
    public class MongoDBHelper
    {
        private readonly IMongoDatabase _database;

        public MongoDBHelper()
        {
            var connectionString = "mongodb+srv://sachin:sachin989@onewhero-bay-heritage-p.zsc2img.mongodb.net/?appName=Onewhero-Bay-Heritage-Park";
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase("OnewheroBayPark");
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
