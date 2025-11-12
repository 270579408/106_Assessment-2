using System.Windows.Controls;
using System.Windows;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;

namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            lblStatus.Text = "";
            var username = txtUsername.Text.Trim();
            var password = pwdPassword.Password;

            //var user = new UserViewModel().AuthenticateUser(username, password);
            //if (user == null)
            //{
            //    lblStatus.Text = "Invalid username or password.";
            //    return;
            //}

            //if (user.Role == "Admin")
            //{
            //    MessageBox.Show($"Welcome Admin {user.Username}!");

            //}
            //else if (user.Role == "Staff")
            //{
            //    MessageBox.Show($"Welcome Staff {user.Username}!");
 
            //}
            //else
            //{
            //    MessageBox.Show($"Welcome {user.Username}!");

            //    NavigationService?.Navigate(new Home());
            //}
        }

        private void GoRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegisterPage());
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Registration is not implemented yet.");
        }
    }
}
