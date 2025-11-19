using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class EventManagement : Page
    {
        private readonly EventViewModel _eventViewModel;
        private List<Event> _allEvents;

        public EventManagement()
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();
            LoadEvents();
        }

        private void LoadEvents()
        {
            _allEvents = _eventViewModel.GetAllEvents();

            EventsGrid.ItemsSource = _allEvents.Select(e => new
            {
                Id = e.Id,
                Title = e.Title,
                EventDate = e.EventDate.ToString("dd MMM yyyy"),
                CreatedAt = e.CreatedAt.ToString("dd MMM yyyy"),
                Price = e.Price
            }).ToList();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(query))
            {
                LoadEvents();
                return;
            }

            var filtered = _allEvents
                .Where(ev => ev.Title.ToLower().Contains(query))
                .Select(e => new
                {
                    Id = e.Id,
                    Title = e.Title,
                    EventDate = e.EventDate.ToString("dd MMM yyyy"),
                    CreatedAt = e.CreatedAt.ToString("dd MMM yyyy"),
                    Price = e.Price
                }).ToList();

            EventsGrid.ItemsSource = filtered;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            LoadEvents();
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddEventPage());
        }

        private void OpenModifyEventPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ModifyEventPage());
        }

        private void ModifyEventFromRow_Click(object sender, RoutedEventArgs e)
        {
            string eventId = (sender as Button).Tag.ToString();

            this.NavigationService.Navigate(new ModifyEventPage(eventId));
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            string eventId = (sender as Button).Tag.ToString();

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this event?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (confirm == MessageBoxResult.Yes)
            {
                _eventViewModel.DeleteEvent(eventId);
                MessageBox.Show("Event deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadEvents();
            }
        }
    }
}
