using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages
{
    public partial class Help : Page
    {
        public Help()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void InquiryInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            Placeholder_InquiryInput.Visibility = string.IsNullOrWhiteSpace(InquiryInput.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
