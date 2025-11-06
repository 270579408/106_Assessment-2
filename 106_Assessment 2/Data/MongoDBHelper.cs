using MongoDB.Driver;

namespace _106_Assessment_2.Data
{
    public class MongoDBHelper
    {
        private readonly IMongoDatabase _database;

        public MongoDBHelper()
        {
            var connectionString = "mongodb+srv://270579408:S692718m@cluster0.xicz7cs.mongodb.net/?appName=Cluster0";
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase("OnewheroBayPark");
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
