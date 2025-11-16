using System.Windows;
using System.Windows.Controls;
using _106_Assessment_2.View.UserControls;

namespace _106_Assessment_2.View.Pages
{
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();

            ProfileUserInitial.Text = GlobalData.CurrentUserName.Length > 0 ? GlobalData.CurrentUserName[0].ToString().ToUpper() : "";

            ProfileUserName.Text = GlobalData.CurrentUserName;
            ProfileUserEmail.Text = GlobalData.CurrentUserEmail;

            ProfileContentArea.Content = new ProfileCommunity();
            Community_Btn.Background = System.Windows.Media.Brushes.LightGray;

            DataContext = this;
        }

        private void Community_Click(object sender, RoutedEventArgs e)
        {
            ProfileContentArea.Content = new ProfileCommunity();
            Community_Btn.Background = System.Windows.Media.Brushes.LightGray;
            Booking_Btn.Background = System.Windows.Media.Brushes.Transparent;
        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            ProfileContentArea.Content = new ProfileBooking();
            Booking_Btn.Background = System.Windows.Media.Brushes.LightGray;
            Community_Btn.Background = System.Windows.Media.Brushes.Transparent;
        }

    }
}
