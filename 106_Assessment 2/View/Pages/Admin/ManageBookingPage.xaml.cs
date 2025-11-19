using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class ManageBookingPage : Page
    {
        private BookingViewModel _bookingViewModel;
        private EventViewModel _eventViewModel;
        private List<Booking> _allBookings;

        public ManageBookingPage()
        {
            InitializeComponent();
            _bookingViewModel = new BookingViewModel();
            _eventViewModel = new EventViewModel();
            LoadBookings();
        }

        // Load all bookings and display
        private void LoadBookings()
        {
            _allBookings = _bookingViewModel.GetAllBooking();
            var bookingDisplay = _allBookings.Select(b => new
            {
                Id = b.Id,
                BookingName = b.BookingName,
                EventTitle = _eventViewModel.GetEventById(b.EventId)?.Title ?? "Unknown Event",
                EventDate = b.EventDate.ToString("dd MMM yyyy"),
            }).ToList();

            BookingsGrid.ItemsSource = bookingDisplay;
        }

        // -------------------------------
        // Cancel Booking
        // -------------------------------
        private void CancelBookingButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string bookingId = button.Tag.ToString();

            var result = MessageBox.Show("Are you sure you want to cancel this booking?",
                                         "Confirm Cancel", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _bookingViewModel.DeleteBooking(bookingId); // implement DeleteBooking in BookingViewModel
                MessageBox.Show("Booking canceled successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadBookings(); // refresh
            }
        }

        // -------------------------------
        // Search Bookings
        // -------------------------------
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text.Trim().ToLower();
            var filtered = _allBookings.Where(b =>
            {
                var eventTitle = _eventViewModel.GetEventById(b.EventId)?.Title.ToLower() ?? "";
                return b.BookingName.ToLower().Contains(query) || eventTitle.Contains(query);
            }).Select(b => new
            {
                Id = b.Id,
                BookingName = b.BookingName,
                EventTitle = _eventViewModel.GetEventById(b.EventId)?.Title ?? "Unknown Event",
                EventDate = b.EventDate.ToString("dd MMM yyyy"),
            }).ToList();

            BookingsGrid.ItemsSource = filtered;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            LoadBookings();
        }
    }
}
