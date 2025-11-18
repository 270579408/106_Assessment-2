using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _106_Assessment_2;

namespace _106_Assessment_2.Common
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private bool _isCollapsed = false;

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            double newWidth = _isCollapsed ? 300 : 65;
            NavColumn.Width = new GridLength(newWidth);

            AppTitle.Visibility = _isCollapsed ? Visibility.Visible : Visibility.Collapsed;

            _isCollapsed = !_isCollapsed;
        }


        private void Dashboard_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Admin.DashBoard());
        private void VisitorMan_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Admin.VisitorManagement());
        private void EventMan_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Admin.EventManagement());
        private void BookingMan_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Admin.BookingManagement());
        private void CommunityMod_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Admin.CommunityModeration());
        private void Report_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new View.Pages.Admin.Reports());

    }
}