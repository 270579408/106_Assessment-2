using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Collections.ObjectModel;

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
                for(int j = 0; j < AllEvents[i].Tags.Count; j++)
                {
                    if(!Tags.Contains(AllEvents[i].Tags[j].ToLower()))
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


        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _startPoint = e.GetPosition(TagScrollViewer);
            _startOffset = TagScrollViewer.HorizontalOffset;
            TagScrollViewer.CaptureMouse();
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging) return;

            Point currentPoint = e.GetPosition(TagScrollViewer);
            double delta = _startPoint.X - currentPoint.X;

            TagScrollViewer.ScrollToHorizontalOffset(_startOffset + delta);
        }

        private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            TagScrollViewer.ReleaseMouseCapture();
        }
    }
}
