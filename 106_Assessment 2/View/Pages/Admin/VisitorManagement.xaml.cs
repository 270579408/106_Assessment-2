using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _106_Assessment_2.ViewModels;
using _106_Assessment_2.Models;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class VisitorManagement: Page
    {
        private RegisterViewModel _userViewModel;
        private List<User> _allUsers;

        public VisitorManagement()
        {
            InitializeComponent();
            _userViewModel = new RegisterViewModel();
            LoadVisitors();
        }

        // Load visitors into DataGrid
        private void LoadVisitors()
        {
            _allUsers = _userViewModel.GetAllUser();
            var data = _allUsers.Select(u => new
            {
                Id = u.ID,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt.ToString("dd MMM yyyy")
            }).ToList();

            VisitorsGrid.ItemsSource = data;
        }

        // Search visitor
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text.Trim().ToLower();

            var filtered = _allUsers.Where(u =>
                u.Name.ToLower().Contains(query) ||
                u.Email.ToLower().Contains(query))
            .Select(u => new
            {
                Id = u.ID,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt.ToString("dd MMM yyyy")
            }).ToList();

            VisitorsGrid.ItemsSource = filtered;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            LoadVisitors();
        }

        // Delete visitor
        private void DeleteVisitorButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = (sender as Button).Tag.ToString();

            var confirm = MessageBox.Show("Are you sure you want to delete this visitor?",
                                          "Confirm Delete",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                _userViewModel.DeleteUser(userId);
                MessageBox.Show("Visitor deleted successfully!");
                LoadVisitors();
            }
        }
    }
}
