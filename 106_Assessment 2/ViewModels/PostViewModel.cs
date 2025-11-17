using MongoDB.Driver;
using _106_Assessment_2.Data;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.ViewModels
{
    public class PostViewModel
    {
        private readonly IMongoCollection<Post> _posts;

        public PostViewModel()
        {
            var dbHelper = new MongoDBHelper();
            _posts = dbHelper.GetCollection<Post>("Posts");
        }

        public void AddPost(Post e)
        {
            _posts.InsertOne(e);
        }

        public List<Post> GetAllPosts()
        {
            return _posts.Find(_ => true).ToList();
        }
    }
}