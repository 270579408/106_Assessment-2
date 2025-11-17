using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using _106_Assessment_2.Models;
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

        private PostViewModel _postViewModel;

        public Community()
        {
            InitializeComponent();

            _postViewModel = new PostViewModel();

            Posts = _postViewModel.GetAllPosts();
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

            Post iniPost = new Post()
            {
                ImageUrl = UploadToCloudinary(SelectedImageUrl),
                Text = MessageInput.Text,
                UploaderId = GlobalData.CurrentUserId,
                ReactorId = new List<string> { },
                PostedDate = DateTime.Now
            };

            _postViewModel.AddPost(iniPost);

            MessageInput.Clear();
            SelectedImage.Source = null;
            SelectedImageRow.Visibility = Visibility.Hidden;
            SelectedImageUrl = "";
        }

        private string UploadToCloudinary(string filePath)
        {
            Account account = new Account(
                "devjm9laj",
                "641983795813514",
                "-a042HqSMoH07m00VXZXSApdTk0");

            Cloudinary cloudinary = new Cloudinary(account);
            string imageUrl = "";

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                Folder = "Onewhero Bay" // optional: organize images
            };

            ImageUploadResult result = cloudinary.Upload(uploadParams);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                imageUrl = result.SecureUrl.ToString();
                // Store imageUrl in MongoDB for global access
            }

            return imageUrl;
        }
    }
}
