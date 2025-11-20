using System.Windows.Controls;
using System.Windows;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using System.Xml.Linq;
using _106_Assessment_2;
using _106_Assessment_2.Common;

namespace _106_Assessment_2.View.Pages
{
    public partial class Login : Page
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly AdministratorViewModel _administratorViewModel;
        public Login()
        {
            InitializeComponent();
            _registerViewModel = new RegisterViewModel();
            _administratorViewModel = new AdministratorViewModel();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(pwdPassword.Password)) FormError.Text = "* All Fields are required.";
            else
            {
                if (txtEmail.Text== "admin@onewhero.co.nz")
                {
                    Administrator admin = _administratorViewModel.GetUserByEmail(txtEmail.Text);

                    if(BCrypt.Net.BCrypt.Verify(pwdPassword.Password, admin.Password))
                    {
                        var adminWindow = new AdminWindow();
                        adminWindow.Show();

                        Window.GetWindow(this)?.Close();
                    } else
                    {
                        FormError.Text = "* Password Not Matched";
                    }
                    return;
                }

                User user = _registerViewModel.GetUserByEmail(txtEmail.Text);
                if (user != null)
                {
                    if(BCrypt.Net.BCrypt.Verify(pwdPassword.Password, user.PasswordHash))
                    {
                        GlobalData.CurrentUserId = user.ID;
                        GlobalData.CurrentUserName = user.Name;
                        GlobalData.CurrentUserEmail = user.Email;

                        var main = new MainWindow();
                        main.Show();

                        Window.GetWindow(this)?.Close();

                        main.ChangeProfileNameSideBar(user.Name);
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