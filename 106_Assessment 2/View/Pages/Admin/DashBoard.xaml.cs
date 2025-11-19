using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class DashBoard : Page
    {
        private BookingViewModel _bookingViewModel;
        private EventViewModel _eventViewModel;
        private RegisterViewModel _registerViewModel;

        private List<Booking> _allBookings;
        private List<Event> _allEvents;
        private List<User> _allUsers;

        public DashBoard()
        {
            InitializeComponent();

            _bookingViewModel = new BookingViewModel();
            _eventViewModel = new EventViewModel();
            _registerViewModel = new RegisterViewModel();

            _allBookings = _bookingViewModel.GetAllBooking();
            _allEvents = _eventViewModel.GetAllEvents();
            _allUsers = _registerViewModel.GetAllUser();

            LoadDashboardData();
        }

        // ---------------------------
        // MAIN LOADER
        // ---------------------------
        private void LoadDashboardData()
        {
            LoadTotalVisitors();
            LoadUpcomingEvents();
            LoadUpcomingEventBookings();
            LoadVisitorInsights();
            LoadPopularAttractions();
            LoadRecentActivity();
        }

        private void LoadTotalVisitors()
        {
            // TODO: Replace with actual DB value
            int totalVisitors = _allBookings.Count;

            TotalVisitorsText.Text = totalVisitors.ToString();
        }

        private void LoadUpcomingEvents()
        {
            int eventCount = _allEvents
                .Where(b => b.EventDate > DateTime.Today)
                .Count();
            UpcomingEventsText.Text = eventCount.ToString();
        }

        private void LoadUpcomingEventBookings()
        {
            int upcomingBookingCount = _allBookings
                .Where(b => b.EventDate > DateTime.Today)
                .Count();

            TotalUpcomingEventBookingText.Text = upcomingBookingCount.ToString();
        }

        private void LoadVisitorInsights()
        {
            var allBookings = _bookingViewModel.GetAllBooking();

            int todaysVisitors = allBookings
                .Count(b => b.EventDate.Date == DateTime.Today); // count bookings for today
            TodaysVisitorsText.Text = todaysVisitors.ToString();

            DateTime weekStart = DateTime.Today.AddDays(-6); // 7-day window: today and previous 6 days
            int weeklyVisitors = allBookings
                .Count(b => b.EventDate.Date >= weekStart && b.EventDate.Date <= DateTime.Today);
            WeeklyVisitorsText.Text = weeklyVisitors.ToString();
        }

        private void LoadPopularAttractions()
        {
            var allBookings = _bookingViewModel.GetAllBooking();

            // Group bookings by EventId and count them
            var popularEventGroups = allBookings
                .GroupBy(b => b.EventId)
                .OrderByDescending(g => g.Count()) // most booked first
                .ToList();

            if (popularEventGroups.Count > 0)
            {
                // Get the top event details
                var topEventId = popularEventGroups[0].Key;
                var topEvent = _eventViewModel.GetEventById(topEventId);
                TopAttractionText.Text = topEvent?.Title ?? "N/A";
            }
            else
            {
                TopAttractionText.Text = "N/A";
            }

            if (popularEventGroups.Count > 1)
            {
                // Get the second most popular event details
                var secondEventId = popularEventGroups[1].Key;
                var secondEvent = _eventViewModel.GetEventById(secondEventId);
                SecondAttractionText.Text = secondEvent?.Title ?? "N/A";
            }
            else
            {
                SecondAttractionText.Text = "N/A";
            }
        }

        public class RecentActivityModel
        {
            public string VisitorName { get; set; }
            public string Activity { get; set; }
            public DateTime DateTime { get; set; } 
            public string Date { get; set; }
        }


        private void LoadRecentActivity()
        {
            var recentActivities = new List<RecentActivityModel>();

            foreach (var booking in _allBookings)
            {
                recentActivities.Add(new RecentActivityModel
                {
                    VisitorName = booking.BookingName,
                    Activity = "Booked Event",
                    DateTime = booking.BookingDate, 
                });
            }

            foreach (var ev in _allEvents)
            {
                recentActivities.Add(new RecentActivityModel
                {
                    VisitorName = ev.Title,
                    Activity = "Event Created",
                    DateTime = ev.CreatedAt,
                });
            }

            foreach (var user in _allUsers)
            {
                recentActivities.Add(new RecentActivityModel
                {
                    VisitorName = user.Name,
                    Activity = "Registered Account",
                    DateTime = user.CreatedAt,
                });
            }

            var sortedActivities = recentActivities
                .OrderBy(a => a.DateTime)
                .Take(10) 
                .ToList();

            foreach (var activity in sortedActivities)
            {
                activity.Date = activity.DateTime.ToString("dd MMM yyyy");
            }

            RecentActivityGrid.ItemsSource = sortedActivities;
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to AddEventPage
            var addEventPage = new AddEventPage();
            this.NavigationService.Navigate(addEventPage);
        }

        private void ModifyEventButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to AddEventPage
            var moddifyEventPage = new ModifyEventPage();
            this.NavigationService.Navigate(moddifyEventPage);
        }

    }

}
