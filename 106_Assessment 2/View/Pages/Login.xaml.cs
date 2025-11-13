using System.Windows.Controls;
using System.Windows;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using System.Xml.Linq;
using _106_Assessment_2;

namespace _106_Assessment_2.View.Pages
{
    public partial class Login : Page
    {
        private readonly RegisterViewModel _registerViewModel;
        public Login()
        {
            InitializeComponent();
            _registerViewModel = new RegisterViewModel();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(pwdPassword.Password)) FormError.Text = "* All Fields are required.";
            else
            {
                User user = _registerViewModel.GetUserByEmail(txtEmail.Text);

                if(user != null)
                {
                    if(BCrypt.Net.BCrypt.Verify(pwdPassword.Password, user.PasswordHash))
                    {
                        GlobalData.CurrentUserId = user.ID;
                        GlobalData.CurrentUserName = user.Name;
                        GlobalData.CurrentUserEmail = user.Email;

                        MessageBox.Show(GlobalData.CurrentUserId);

                    } else
                    {
                        FormError.Text = "* Password Not Matched";
                    }
                } else
                {
                    FormError.Text = "* User not registered.";
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Register());
        }
    }
}