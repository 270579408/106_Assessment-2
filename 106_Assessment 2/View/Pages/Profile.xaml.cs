using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using _106_Assessment_2.View.UserControls;

namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.CurrentUserId = "";
            GlobalData.CurrentUserName = "";
            GlobalData.CurrentUserEmail = "";
            var main = new MainWindow();
            main.Show();
            main.ChangeProfileNameSideBar("Login");
        }
    }
}
