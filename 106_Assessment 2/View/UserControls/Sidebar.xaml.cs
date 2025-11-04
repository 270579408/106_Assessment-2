using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.UserControls
{
    /// <summary>
    /// Interaction logic for Sidebar.xaml
    /// </summary>
    public partial class Sidebar : UserControl
    {
        public event Action<string> PageSelected;
        public Sidebar()
        {
            InitializeComponent();
        }

        private void home_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("home");
        }

        private void events_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("events");
        }

        private void activities_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("activities");
        }

        private void parkInfo_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("parkInfo");
        }

        private void community_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("community");
        }

        private void help_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("help");
        }

        private void account_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("account");
        }

        private void settings_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("settings");
        }
    }
}
