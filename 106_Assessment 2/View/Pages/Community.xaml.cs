using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using _106_Assessment_2.Models;
using _106_Assessment_2.Common;
using _106_Assessment_2.ViewModels;


namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for Community.xaml
    /// </summary>

    public partial class Community : System.Windows.Controls.Page
    {
        public List<Post> Posts { get; set; }

        public string SelectedImageUrl;

        public PostViewModel _postViewModel;

        public Community()
        {
            InitializeComponent();

            _postViewModel = new PostViewModel();

            try
            {
                Posts = _postViewModel.GetAllPosts();
                Posts.Reverse();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading posts: " + ex.Message);
            }
            PostItemControl.ItemsSource = Posts;

            DataContext = this;
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            Placeholder_Input.Visibility = string.IsNullOrWhiteSpace(MessageInput.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.png;*.bmp;*.gif)|*.jpg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                if(openFileDialog.FileName != null)
                {
                    SelectedImage.Source = SelectedImage.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    SelectedImageRow.Visibility = Visibility.Visible;
                    SelectedImageUrl = openFileDialog.FileName;
                }
            }
        }
        private void RemoveImage_Click(object sender, RoutedEventArgs e)
        {
            SelectedImage.Source = null;
            SelectedImageRow.Visibility = Visibility.Hidden;
        }


        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalData.CurrentUserId == null &&
                GlobalData.CurrentUserEmail == null &&
                GlobalData.CurrentUserName == null)
            {
                MessageBox.Show("You need to Login to upload.");
                return;
            }

            string uploadedImageUrl = null;

            if (!string.IsNullOrWhiteSpace(SelectedImageUrl))
            {
                uploadedImageUrl = Helpers.UploadToCloudinary(SelectedImageUrl, "Post");
            }

            Post iniPost = new Post()
            {
                ImageUrl = uploadedImageUrl,
                Text = MessageInput.Text,
                UploaderId = GlobalData.CurrentUserId,
                ReactorId = new List<string>(),
                PostedDate = DateTime.Now
            };

            _postViewModel.AddPost(iniPost);

            Posts = _postViewModel.GetAllPosts();
            Posts.Reverse();

            PostItemControl.ItemsSource = null;  
            PostItemControl.ItemsSource = Posts;

            MessageInput.Clear();
            SelectedImage.Source = null;
            SelectedImageRow.Visibility = Visibility.Hidden;
            SelectedImageUrl = "";
        }


        
    }
}
