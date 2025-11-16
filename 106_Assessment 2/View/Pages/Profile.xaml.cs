using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages
{
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Community_Click(object sender, RoutedEventArgs e)
        {
            ProfileContentArea.Content = new ProfilelCommunity();
        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            ProfileContentArea.Content = new ProfileBooking();
        }

    }
}
