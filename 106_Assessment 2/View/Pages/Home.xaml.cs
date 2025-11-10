using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using MongoDB.Driver;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace _106_Assessment_2.View.Pages
{
    public partial class Home : Page
    {
        private readonly EventViewModel _eventViewModel;
        private readonly ActivityViewModel _activityViewModel;

        private Point _startPoint;
        private double _startOffset;
        private bool _isDragging = false;

        public List<Event> AllEvents { get; set; }
        private List<Event> FeaturedEvents;
        public List<Activity> AllActivities { get; set; }

        public Home()
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();
            _activityViewModel = new ActivityViewModel();

            AllEvents = _eventViewModel.GetAllEvents();
            FeaturedEvents = AllEvents
                .Where(ev => ev.Featured)
                .ToList();

            AllActivities = _activityViewModel.GetAllActivities();

            FeaturedBanner.Banner = FeaturedEvents;

            FeaturedBanner.ShowBanner(FeaturedBanner._currentIndex);
            FeaturedBanner.StartBannerRotation();

            DataContext = this;
        }

        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _startPoint = e.GetPosition(EventScrollViewer);
            _startOffset = EventScrollViewer.HorizontalOffset;
            EventScrollViewer.CaptureMouse();
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging) return;

            Point currentPoint = e.GetPosition(EventScrollViewer);
            double delta = _startPoint.X - currentPoint.X;

            EventScrollViewer.ScrollToHorizontalOffset(_startOffset + delta);
        }

        private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            EventScrollViewer.ReleaseMouseCapture();
        }
    }
}
