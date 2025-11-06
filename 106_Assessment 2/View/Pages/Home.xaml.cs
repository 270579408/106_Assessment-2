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

        public List<Event> AllEvents { get; set; }
        private List<Event> FeaturedEvents;

        public Home()
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();

            AllEvents = _eventViewModel.GetAllEvents();
            FeaturedEvents = AllEvents
                .Where(ev => ev.Tag != null && ev.Tag.Contains("f"))
                .ToList();

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
    }
}
