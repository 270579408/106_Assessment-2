using _106_Assessment_2.Models;
using _106_Assessment_2.View.Pages;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            if (Banner == null || Banner.Count == 0) return;
            if (index < 0 || index >= Banner.Count) return;

            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.35));

            fadeOut.Completed += (s, e) =>
            {
                // Update UI
                var ev = Banner[index];
                FeaturedEventsBannerImg.Source = new BitmapImage(new Uri(ev.ImageUrl, UriKind.Absolute));
                FeaturedEventsTitle.Text = ev.Title;
                FeaturedEventsDescription.Text = ev.Description;

                // Make sure Book button has the event in Tag so click can read it
                bookNow_button.Tag = ev;

                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.35));
                FeaturedEventsBannerImg.BeginAnimation(OpacityProperty, fadeIn);
            };

            FeaturedEventsBannerImg.BeginAnimation(OpacityProperty, fadeOut);
        }

        public void StartBannerRotation()
        {
            if (Banner == null || Banner.Count == 0) return;

            // show first immediately
            _currentIndex = 0;
            ShowBanner(_currentIndex);

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
            if (Banner == null || Banner.Count == 0) return;
            _currentIndex = (_currentIndex - 1 + Banner.Count) % Banner.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }

        private void nextButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Banner == null || Banner.Count == 0) return;
            _currentIndex = (_currentIndex + 1) % Banner.Count;
            ShowBanner(_currentIndex);
            ResetBannerTimer();
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var selectedEvent = btn?.Tag as Event;

            // If Tag wasn't set (defensive), try using current index
            if (selectedEvent == null && Banner != null && Banner.Count > 0 && _currentIndex >= 0 && _currentIndex < Banner.Count)
                selectedEvent = Banner[_currentIndex];

            if (selectedEvent == null)
            {
                // nothing to navigate to
                return;
            }

            // Try to find a containing Frame (search visual tree for ancestor Frame)
            Frame frame = FindAncestor<Frame>(this);

            // Fallback: try MainWindow's named frame "MainFrame"
            if (frame == null)
            {
                var mw = Application.Current?.MainWindow;
                if (mw != null)
                {
                    // try to find a field/property named MainFrame via logical tree lookup
                    var found = mw.FindName("MainFrame") as Frame;
                    if (found != null)
                        frame = found;
                }
            }

            if (frame != null)
            {
                frame.Navigate(new BookEvent(selectedEvent));
            }
            else
            {
                // as a last resort, try NavigationService from parent Page (may be null, but try)
                var nav = System.Windows.Navigation.NavigationService.GetNavigationService(this);
                if (nav != null)
                {
                    nav.Navigate(new BookEvent(selectedEvent));
                }
                else
                {
                    // final fallback: show error/debug so you know why it failed
                    MessageBox.Show("Navigation failed: no Frame or NavigationService found.", "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // Helper to find ancestor in visual tree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T) return (T)current;
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }
    }
}
