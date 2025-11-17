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
        public AdminWindow()
        {
            InitializeComponent();
            AdminFrame.Content = new View.Pages.Admin.AdminDashboardPage();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new View.Pages.Admin.AdminDashboardPage();
        }

        private void EventManagement_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new View.Pages.Admin.EventManagementPage();
        }

        private void VisitorManagement_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new View.Pages.Admin.VisitorManagementPage();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new View.Pages.Admin.ReportsPage();
        }

        private void MessageCenter_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new View.Pages.Admin.MessageCenterPage();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        { 
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}