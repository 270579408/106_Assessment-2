using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for Community.xaml
    /// </summary>
    public partial class Community : System.Windows.Controls.Page
    {

        public Community()
        {
            InitializeComponent();

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
                UploadToCloudinary(openFileDialog.FileName);
            }
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("a");
        }

        private void UploadToCloudinary(string filePath)
        {
            Account account = new Account(
                "devjm9laj",
                "641983795813514",
                "-a042HqSMoH07m00VXZXSApdTk0");

            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                Folder = "Onewhero Bay" // optional: organize images
            };

            ImageUploadResult result = cloudinary.Upload(uploadParams);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string imageUrl = result.SecureUrl.ToString();
                MessageBox.Show("Uploaded! URL: " + imageUrl);
                // Store imageUrl in MongoDB for global access
            }
            else
            {
                MessageBox.Show("Upload failed: " + result.Error.Message);
            }
        }
    }
}
