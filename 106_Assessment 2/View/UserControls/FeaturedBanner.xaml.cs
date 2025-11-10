using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace _106_Assessment_2.View.UserControls
{

    public partial class FeaturedBanner : UserControl
    {
        private DispatcherTimer _timer;
        public int _currentIndex = 0;

        public List<Event> Banner;

        public FeaturedBanner()
        {
            InitializeComponent();
        }

        public void ShowBanner(int index)
        {
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));

            fadeOut.Completed += (s, e) =>
            {
                FeaturedEventsBannerImg.Source = new BitmapImage(new Uri(Banner[index].ImageUrl, UriKind.Absolute));
                FeaturedEventsTitle.Text = Banner[index].Title;
                FeaturedEventsDescription.Text = Banner[index].Description;

                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
                FeaturedEventsBannerImg.BeginAnimation(OpacityProperty, fadeIn);
            };
            FeaturedEventsBannerImg.BeginAnimation(OpacityProperty, fadeOut);
        }

        public void StartBannerRotation()
        {
            if (_timer != null)
                _timer.Stop();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += (s, e) =>
            {
                _currentIndex = (_currentIndex + 1) % Banner.Count;
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
            _currentIndex = (_currentIndex - 1 + Banner.Count) % Banner.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }

        private void nextButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _currentIndex = (_currentIndex + 1) % Banner.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }
    }
}
