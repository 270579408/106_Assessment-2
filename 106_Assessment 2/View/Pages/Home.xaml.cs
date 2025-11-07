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

        private DispatcherTimer _timer;
        private int _currentIndex = 0;

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

            ShowBanner(_currentIndex);
            StartBannerRotation();

            DataContext = this;
        }

        private void ShowBanner(int index)
        {
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));

            fadeOut.Completed += (s, e) =>
            {
                FeaturedEventsBannerImg.Source = new BitmapImage(new Uri(FeaturedEvents[index].ImageUrl, UriKind.Absolute));
                FeaturedEventsTitle.Text = FeaturedEvents[index].Title;
                FeaturedEventsDescription.Text = FeaturedEvents[index].Description;

                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
                FeaturedEventsBannerImg.BeginAnimation(OpacityProperty, fadeIn);
            };
            FeaturedEventsBannerImg.BeginAnimation(OpacityProperty, fadeOut);
        }

        private void StartBannerRotation()
        {
            if (_timer != null)
                _timer.Stop(); 

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(10); 
            _timer.Tick += (s, e) =>
            {
                _currentIndex = (_currentIndex + 1) % FeaturedEvents.Count;
                ShowBanner(_currentIndex);
            };
            _timer.Start();
        }

        private void ResetBannerTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Start(); 
            }
        }

        private void prevButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _currentIndex = (_currentIndex - 1 + FeaturedEvents.Count) % FeaturedEvents.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }

        private void nextButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _currentIndex = (_currentIndex + 1) % FeaturedEvents.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
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
