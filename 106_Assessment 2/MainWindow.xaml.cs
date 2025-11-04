using System.Windows;

namespace _106_Assessment_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SidebarControl.PageSelected += SidebarControl_PageSelected;
            MainFrame.Navigate(new View.Pages.Home());
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

                default:
                    break;

            }
        }
    }
}