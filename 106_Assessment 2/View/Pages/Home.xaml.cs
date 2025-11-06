using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Media.Imaging;


class Banner
{
    public string Id;
    public string ImageUrl;
    public string Title;
    public string Description;
    public string EventDate;
    public string CurrentDate;
    public string tag;
}

namespace _106_Assessment_2.View.Pages
{
    public partial class Home : Page
    {
        private List<Banner> _banners = new List<Banner>()
            {
                new Banner
                {
                    Id = "1",
                    ImageUrl = "/Resources/event1.jpg",
                    Title = "Summer Festival",
                    Description = "Join us for a day of fun, food, and festivities at the annual Summer Festival!",
                    EventDate = "2024-07-15",
                    CurrentDate = "2024-06-01",
                    tag = "Festival"
                },
                new Banner
                {
                    Id = "2",
                    ImageUrl = "/Resources/event2.jpg",
                    Title = "Art in the Park",
                    Description = "Experience the beauty of local art and creativity at our Art in the Park event.",
                    EventDate = "2024-08-10",
                    CurrentDate = "2024-06-01",
                    tag = "Art"
                },
                new Banner
                {
                    Id = "3",
                    ImageUrl = "/Resources/event3.jpg",
                    Title = "Music Under the Stars",
                    Description = "Enjoy an evening of live music performances under the night sky.",
                    EventDate = "2024-09-05",
                    CurrentDate = "2024-06-01",
                    tag = "Music"
                },
                new Banner
                {
                    Id = "4",
                    ImageUrl = "/Resources/event4.jpg",
                    Title = "Food Truck Fiesta",
                    Description = "Savor delicious dishes from a variety of food trucks at our Food Truck Fiesta.",
                    EventDate = "2024-07-25",
                    CurrentDate = "2024-06-01",
                    tag = "Food"
                }
            };

        private DispatcherTimer _timer;
        private int _currentIndex = 0;

        public Home()
        {
            InitializeComponent();

            ShowBanner(_currentIndex);
            StartBannerRotation();
        }

        private void ShowBanner(int index)
        {
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
            fadeOut.Completed += (s, e) =>
            {
                FeaturedEventsBannerImg.Source = new BitmapImage(new Uri(_banners[index].ImageUrl, UriKind.Relative));
                FeaturedEventsTitle.Text = _banners[index].Title;
                FeaturedEventsDescription.Text = _banners[index].Description;

                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
                FeaturedEventsBannerImg.BeginAnimation(OpacityProperty, fadeIn);
            };
            FeaturedEventsBannerImg.BeginAnimation(OpacityProperty, fadeOut);
        }

        private void StartBannerRotation()
        {
            if (_timer != null)
                _timer.Stop(); // stop existing timer

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(4); // rotate every 4 seconds
            _timer.Tick += (s, e) =>
            {
                _currentIndex = (_currentIndex + 1) % _banners.Count;
                ShowBanner(_currentIndex);
            };
            _timer.Start();
        }

        private void ResetBannerTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Start(); // restart timer
            }
        }

        private void prevButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _currentIndex = (_currentIndex - 1 + _banners.Count) % _banners.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }

        private void nextButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _currentIndex = (_currentIndex + 1) % _banners.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }
    }
}
