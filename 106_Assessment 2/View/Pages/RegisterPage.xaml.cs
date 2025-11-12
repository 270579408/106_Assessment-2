using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            lblStatus.Text = "";
            var username = txtUsername.Text.Trim();
            var email = txtEmail.Text.Trim();
            var pw = pwdPassword.Password;
            var pw2 = pwdConfirm.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pw))
            {
                lblStatus.Text = "Please fill all fields.";
                return;
            }
            if (pw != pw2)
            {
                lblStatus.Text = "Passwords do not match.";
                return;
            }

            //var (ok, msg) = new UserViewModel().RegisterUser(username, email, pw);
            //if (!ok)
            //{
            //    lblStatus.Text = msg;
            //    return;
            //}

            MessageBox.Show("Registration successful. Please sign in.", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService?.Navigate(new Account());
        }

        private void GoLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Account());
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigating to Sign In page.", "Info",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}