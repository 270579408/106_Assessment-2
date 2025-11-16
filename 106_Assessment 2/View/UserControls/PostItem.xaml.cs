using _106_Assessment_2.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _106_Assessment_2.View.UserControls
{
    public partial class PostItem : UserControl
    {
        public bool Reacetd = false;

        public PostItem()
        {
            InitializeComponent();
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
            if (control != null)
            {
                if (e.NewValue is Post post)
                {
                    control.PostTextContent.Text = post.Text;
                }
            }
        }
    }
}
