using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using MongoDB.Driver;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace _106_Assessment_2.View.Pages
{
    public partial class Home : Page
    {
        private readonly EventViewModel _eventViewModel;

        private DispatcherTimer _timer;
        private int _currentIndex = 0;

        private List<Event> _featuredEvents;

        public Home()
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();

            _featuredEvents = _eventViewModel.GetAllEvents();
            _featuredEvents = _featuredEvents
                .Where(ev => ev.Tag != null && ev.Tag.Contains("f"))
                .ToList();

            ShowBanner(_currentIndex);
            StartBannerRotation();
        }

        private void ShowBanner(int index)
        {
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));

            fadeOut.Completed += (s, e) =>
            {
                FeaturedEventsBannerImg.Source = new BitmapImage(new Uri(_featuredEvents[index].ImageUrl, UriKind.Absolute));
                FeaturedEventsTitle.Text = _featuredEvents[index].Title;
                FeaturedEventsDescription.Text = _featuredEvents[index].Description;

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
                _currentIndex = (_currentIndex + 1) % _featuredEvents.Count;
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
            _currentIndex = (_currentIndex - 1 + _featuredEvents.Count) % _featuredEvents.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }

        private void nextButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _currentIndex = (_currentIndex + 1) % _featuredEvents.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }
    }
}
