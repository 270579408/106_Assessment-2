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

            DataContext = this;
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Searchbar.Text == "")
                Placeholder_Searchbar.Visibility = System.Windows.Visibility.Visible;
            else
                Placeholder_Searchbar.Visibility = System.Windows.Visibility.Hidden;

            var toRemove = AllEvents.Where(ev => ev.Featured).ToList();

            foreach(var ev in toRemove)
            {
                AllEvents.Remove(ev);
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
