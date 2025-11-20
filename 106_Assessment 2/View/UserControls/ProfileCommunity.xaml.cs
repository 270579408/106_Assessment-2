using _106_Assessment_2.ViewModels;
using _106_Assessment_2.Models;
using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.UserControls
{
    /// <summary>
    /// Interaction logic for ProfileCommunity.xaml
    /// </summary>
    public partial class ProfileCommunity : UserControl
    {
        public List<Post> Posts { get; set; }

        public PostViewModel _postViewModel;

        public ProfileCommunity()
        {
            InitializeComponent();
            _postViewModel = new PostViewModel();

            try
            {
                Posts = _postViewModel.GetPostsById(GlobalData.CurrentUserId);
                Posts.Reverse();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading posts: " + ex.Message);
            }

            PostItemControl.ItemsSource = Posts;

            DataContext = this;
        }
    }
}
