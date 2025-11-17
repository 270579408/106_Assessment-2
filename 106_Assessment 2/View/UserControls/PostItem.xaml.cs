using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _106_Assessment_2.View.UserControls
{
    public partial class PostItem : UserControl
    {
        public bool Reacetd = false;

        public RegisterViewModel _registerViewModel;

        public User userDetails;

        public PostItem()
        {
            InitializeComponent();

            _registerViewModel = new RegisterViewModel();

        }

        private void ReactBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Reacetd = !Reacetd;

            ReactBtnImg.Source = Reacetd ? new BitmapImage(new Uri("pack://application:,,,/Resources/heart-red-filled.png")) : 
                new BitmapImage(new Uri("pack://application:,,,/Resources/heart-outline.png"));
        }

        public Post Data
        {
            get { return (Post)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                "Data",
                typeof(Post),
                typeof(PostItem),
                new PropertyMetadata(null, OnDataChanged));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PostItem;
            if (control == null) return;

            if (e.NewValue is Post post)
            {
                var registerVM = control._registerViewModel;
                var userDetails = registerVM.GetUserById(post.UploaderId);

                control.PostTextContent.Text = post.Text;

                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    control.PostImageContent.Source = new BitmapImage(
                        new Uri(post.ImageUrl, UriKind.Absolute));
                }

                control.PostUserInitial.Text = userDetails?.Name?[0].ToString() ?? "?";
                control.PostUserName.Text = userDetails?.Name ?? "Unknown";
                control.PostTimestamp.Text = post.PostedDate.ToString("dd MMM yyyy, hh:mm tt");
            }
        }
    }
}
