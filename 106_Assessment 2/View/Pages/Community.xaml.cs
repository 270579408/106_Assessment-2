using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for Community.xaml
    /// </summary>
    public partial class Community : Page
    {
        public Community()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Placeholder_Searchbar.Visibility = string.IsNullOrWhiteSpace(Searchbar.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
