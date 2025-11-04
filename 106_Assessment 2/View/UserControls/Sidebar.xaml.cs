using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.UserControls
{
    /// <summary>
    /// Interaction logic for Sidebar.xaml
    /// </summary>
    public partial class Sidebar : UserControl
    {
        public event Action<string> PageSelected;
        public Sidebar()
        {
            InitializeComponent();
        }

        private void home_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("home");
        }

        private void events_clicked(object sender, RoutedEventArgs e)
        {
            PageSelected?.Invoke("events");
        }
    }
}
