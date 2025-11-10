using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _106_Assessment_2.View.Pages
{
    public partial class Events : Page
    {
        private readonly EventViewModel _eventViewModel;
        public ObservableCollection<Event> AllEvents { get; set; } = new ObservableCollection<Event>();
        public ObservableCollection<Event> FilteredEvents { get; set; } = new ObservableCollection<Event>();
        public List<string> Tags { get; set; } = new List<string>();

        private Point _startPoint;
        private double _startOffset;
        private bool _isDragging = false;

        public Events()
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();
            AllEvents = new ObservableCollection<Event>(_eventViewModel.GetAllEvents());
            for (int i = 0; i < AllEvents.Count; i++)
            {
                for (int j = 0; j < AllEvents[i].Tags.Count; j++)
                {
                    if (!Tags.Contains(AllEvents[i].Tags[j].ToLower()))
                        Tags.Add(AllEvents[i].Tags[j].ToLower());
                }
            }
            FilteredEvents = new ObservableCollection<Event>(AllEvents);
            DataContext = this;
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Placeholder_Searchbar.Visibility = string.IsNullOrWhiteSpace(Searchbar.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
            string searchText = Searchbar.Text.ToLower();
            FilteredEvents.Clear();
            foreach (var ev in AllEvents)
            {
                if (ev.Tags.Any(t => t.ToLower().Contains(searchText)) ||
                    ev.Title.ToLower().Contains(searchText))
                {
                    FilteredEvents.Add(ev);
                }
            }
        }

        private void Tag_Click(object sender, RoutedEventArgs e)
        {
            if (_isDragging) return;
            if (sender is Button btn)
            {
                string btnContent = btn.Content.ToString().ToLower();
                FilteredEvents.Clear();
                foreach (var ev in AllEvents)
                {
                    if (ev.Tags.Any(t => t.ToLower().Contains(btnContent)) ||
                        ev.Title.ToLower().Contains(btnContent))
                    {
                        FilteredEvents.Add(ev);
                    }
                }
            }
            MessageBox.Show("Tag clicked!");
        }

        private bool IsEventSourceInside<T>(DependencyObject source) where T : DependencyObject
        {
            while (source != null)
            {
                if (source is T) return true;
                source = VisualTreeHelper.GetParent(source);
            }
            return false;
        }

        private void LeftArrowButton_Click(object sender, RoutedEventArgs e)
        {
            double newOffset = TagScrollViewer.HorizontalOffset - 150;
            if (newOffset < 0) newOffset = 0;
            TagScrollViewer.ScrollToHorizontalOffset(newOffset);
        }

        private void RightArrowButton_Click(object sender, RoutedEventArgs e)
        {
            double maxOffset = TagScrollViewer.ScrollableWidth;
            double newOffset = TagScrollViewer.HorizontalOffset + 150;
            if (newOffset > maxOffset) newOffset = maxOffset;
            TagScrollViewer.ScrollToHorizontalOffset(newOffset);
        }

    }
}

