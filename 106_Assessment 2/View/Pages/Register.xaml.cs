using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using _106_Assessment_2.Common;
using BCrypt.Net;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;

namespace _106_Assessment_2.View.Pages
{
    public partial class Register : Page
    {
        private readonly RegisterViewModel _registerViewModel;

        public Register()
        {
            InitializeComponent();

            _registerViewModel = new RegisterViewModel();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(pwdPassword.Password)) FormError.Text = "* All Fields are required.";
            else if(!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) FormError.Text = "* Please Enter Valid Email Address.";
            else if(pwdPassword.Password != pwdConfirm.Password) FormError.Text = "* Password doesn't match with confirm password.";
            else if(!Helpers.IsPasswordValid(pwdPassword.Password)) FormError.Text = "* Password is not valid.";
            else
            {
                FormError.Text = "";

                string passHashed = BCrypt.Net.BCrypt.HashPassword(pwdPassword.Password);

                User visitor = new User
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    PasswordHash = passHashed
                };

                _registerViewModel.AddUser(visitor);
                MessageBox.Show("User register Successfully.");
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Login());
        }
    }
}