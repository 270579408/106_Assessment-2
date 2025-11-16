using System.Security.Cryptography.X509Certificates;
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
    }
}
