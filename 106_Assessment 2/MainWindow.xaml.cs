using System.Windows;
using _106_Assessment_2.Models;

namespace _106_Assessment_2
{
    public partial class MainWindow : Window
    {
        private bool _isCollapsed = false;
        public User currentUser;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new View.Pages.Help());
        }

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            double newWidth = _isCollapsed ? 250 : 65;
            NavColumn.Width = new GridLength(newWidth);

            AppTitle.Visibility = _isCollapsed ? Visibility.Visible : Visibility.Collapsed;

            _isCollapsed = !_isCollapsed;
        }

        private void Home_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Home());
        private void Events_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Events());
        private void Things_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Activities());
        private void Park_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.ParkInfo());
        private void Community_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Community());
        private void Help_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Help());
        private void LogIn_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Login());
        private void Settings_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Settings Page coming soon!");

        public void ChangeProfileNameSideBar(string text)
        {
            LoginSideBarOption.Content = text;
        }
    }
}