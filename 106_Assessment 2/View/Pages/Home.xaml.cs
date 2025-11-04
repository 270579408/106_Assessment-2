using System.Windows;
using System.Windows.Controls;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;


namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private readonly VisitorViewModel _viewModels;

        public Home()
        {
            InitializeComponent();
            _viewModels = new VisitorViewModel();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var visitor = new Visitor
            {
                Name = name_input.Text,
                Email = email_input.Text,
                Phone = phone_input.Text,
                Interest = interest_input.Text
            };

            _viewModels.AddVisitor(visitor);

            MessageBox.Show("Visitor information submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
