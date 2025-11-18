using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using MongoDB.Driver;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

        private void ScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            EventScrollViewer.ScrollToHorizontalOffset(EventScrollViewer.HorizontalOffset - 300);
        }

        private void ScrollRight_Click(object sender, RoutedEventArgs e)
        {
            EventScrollViewer.ScrollToHorizontalOffset(EventScrollViewer.HorizontalOffset + 300);
        }

        private void EventScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            var parent = FindParent<ScrollViewer>(EventScrollViewer);
            if (parent != null)
                parent.ScrollToVerticalOffset(parent.VerticalOffset - e.Delta);
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T correctlyTyped)
                    return correctlyTyped;

                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

    }
}
