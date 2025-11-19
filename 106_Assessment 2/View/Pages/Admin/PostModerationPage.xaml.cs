using System.Windows;
using System.Windows.Controls;
using _106_Assessment_2.Models;
using _106_Assessment_2.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using _106_Assessment_2.ViewModels;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class PostModerationPage : Page
    {
        private PostViewModel _postViewModel;
        private List<Post> _posts;
        public PostModerationPage()
        {
            InitializeComponent();

            _postViewModel = new PostViewModel();
            _posts = _postViewModel.GetAllPosts();
            // Initialize MongoDB collection

            // Load posts into DataGrid
            LoadPosts();
        }

        /// <summary>
        /// Load all posts from MongoDB into the DataGrid
        /// </summary>
        private void LoadPosts()
        {
            PostsDataGrid.ItemsSource = _posts;
        }

        /// <summary>
        /// Delete selected post
        /// </summary>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PostsDataGrid.SelectedItem is Post selectedPost)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this post?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Call DeletePost from the ViewModel
                    bool deleted = _postViewModel.DeletePost(selectedPost.Id);

                    if (deleted)
                    {
                        MessageBox.Show("Post deleted successfully!");
                        // Refresh the local list
                        _posts = _postViewModel.GetAllPosts();
                        LoadPosts();
                    }
                    else
                    {
                        MessageBox.Show("Post could not be deleted.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a post to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
