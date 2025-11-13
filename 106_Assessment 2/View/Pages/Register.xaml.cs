using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages
{
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Registration successful. Please sign in.", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
            //NavigationService?.Navigate(new Account());
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Login());
        }
    }
}