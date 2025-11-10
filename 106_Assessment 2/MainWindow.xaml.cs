using System.Windows;

namespace _106_Assessment_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SidebarControl.PageSelected += SidebarControl_PageSelected;
            MainFrame.Navigate(new View.Pages.Activities());
        }

        private void SidebarControl_PageSelected(string pageName)
        {
            switch(pageName)
            {
                case "home":
                    MainFrame.Navigate(new View.Pages.Home());
                    break;

                case "events":
                    MainFrame.Navigate(new View.Pages.Events());
                    break;

                case "activities":
                    MainFrame.Navigate(new View.Pages.Activities());
                    break;

                case "parkInfo":
                    MainFrame.Navigate(new View.Pages.ParkInfo());
                    break;

                case "community":
                    MainFrame.Navigate(new View.Pages.Community());
                    break;

                case "help":
                    MainFrame.Navigate(new View.Pages.Help());
                    break;

                case "account":
                    MainFrame.Navigate(new View.Pages.Account());
                    break;

                case "settings":
                    MainFrame.Navigate(new View.Pages.Settings());
                    break;

                default:
                    break;

            }
        }
    }
}