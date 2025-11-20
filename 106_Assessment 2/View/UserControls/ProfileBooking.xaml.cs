using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _106_Assessment_2.View.UserControls
{
    /// <summary>
    /// Interaction logic for ProfileBooking.xaml
    /// </summary>
    public partial class ProfileBooking : UserControl
    {
        private BookingViewModel _bookingViewModel;
        private EventViewModel _eventViewModel;
        private List<Booking> _allBookings;

        public ProfileBooking()
        {
            InitializeComponent();
            _bookingViewModel = new BookingViewModel();
            _eventViewModel = new EventViewModel();
            LoadBookings();
        }

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

    }
}
