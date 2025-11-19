using _106_Assessment_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class DashBoard : Page
    {
        private BookingViewModel _booking { get; set; }
        private EventViewModel _event { get; set; }

        public DashBoard()
        {
            InitializeComponent();

            _booking = new BookingViewModel();
            _event = new EventViewModel();

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

        // ---------------------------
        // TOTAL VISITORS
        // ---------------------------
        private void LoadTotalVisitors()
        {
            // TODO: Replace with actual DB value
            int totalVisitors = _booking.GetAllBooking().Count;

            TotalVisitorsText.Text = totalVisitors.ToString();
        }

        // ---------------------------
        // UPCOMING EVENTS
        // ---------------------------
        private void LoadUpcomingEvents()
        {
            // TODO: Replace with actual DB value
            int eventCount = _event.GetAllEvents().Count;

            UpcomingEventsText.Text = eventCount.ToString();
        }

        // ---------------------------
        // UPCOMING EVENT BOOKINGS
        // ---------------------------
        private void LoadUpcomingEventBookings()
        {
            var allBookings = _booking.GetAllBooking(); // your existing method

            // Filter upcoming bookings (event date AFTER today)
            var upcoming = allBookings
                            .Where(b => b.EventDate.Date > DateTime.Now.Date)
                            .ToList();

            TotalUpcomingEventBookingText.Text = upcoming.Count.ToString();
        }

        // ---------------------------
        // VISITOR INSIGHTS
        // ---------------------------
        private void LoadVisitorInsights()
        {
            var allBookings = _booking.GetAllBooking();

            // Today's visitors
            var today = DateTime.Now.Date;
            int todaysCount = allBookings.Count(b => b.EventDate.Date == today);
            TodaysVisitorsText.Text = todaysCount.ToString();

            // Last 7 days (including today)
            var weekAgo = today.AddDays(-6); // 7 days including today
            int weeklyCount = allBookings.Count(b => b.EventDate.Date >= weekAgo && b.EventDate.Date <= today);
            WeeklyVisitorsText.Text = weeklyCount.ToString();
        }

        // ---------------------------
        // POPULAR ATTRACTIONS
        // ---------------------------
        private void LoadPopularAttractions()
        {
            var allBookings = _booking.GetAllBooking();

            if (allBookings.Count == 0)
            {
                TopAttractionText.Text = "No bookings yet";
                SecondAttractionText.Text = "No bookings yet";
                return;
            }

            // Group bookings by EventId and count
            var eventCounts = allBookings
                                .GroupBy(b => b.EventId)
                                .Select(g => new { EventId = g.Key, Count = g.Count() })
                                .OrderByDescending(g => g.Count)
                                .ToList();

            // Top booked event
            if (eventCounts.Count > 0)
            {
                var topEvent = _event.GetEventById(eventCounts[0].EventId);
                TopAttractionText.Text = topEvent?.Title ?? "Unknown Event";
            }

            // Second most booked event
            if (eventCounts.Count > 1)
            {
                var secondEvent = _event.GetEventById(eventCounts[1].EventId);
                SecondAttractionText.Text = secondEvent?.Title ?? "Unknown Event";
            }
            else
            {
                SecondAttractionText.Text = "N/A";
            }
        }
        // ---------------------------
        // RECENT ACTIVITY (DATA TABLE)
        // ---------------------------
        private void LoadRecentActivity()
        {
            // TODO: Replace with DB activity logs
            List<RecentActivityModel> recent = new List<RecentActivityModel>()
            {
                new RecentActivityModel { VisitorName="Alice Johnson", Activity="Booked Event", Date="20 Nov", Status="Confirmed" },
                new RecentActivityModel { VisitorName="Mark Smith", Activity="Registered Account", Date="19 Nov", Status="Active" },
                new RecentActivityModel { VisitorName="Daniel Carter", Activity="Cancelled Booking", Date="19 Nov", Status="Cancelled" },
                new RecentActivityModel { VisitorName="Emily Brown", Activity="Booked Event", Date="18 Nov", Status="Confirmed" }
            };

            RecentActivityGrid.ItemsSource = recent;
        }
    }

    // -------------------------------------
    // DATA MODEL FOR RECENT ACTIVITY TABLE
    // -------------------------------------
    public class RecentActivityModel
    {
        public string VisitorName { get; set; }
        public string Activity { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}
