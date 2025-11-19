using _106_Assessment_2.Common;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using Microsoft.Win32;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class ModifyEventPage : Page
    {
        private readonly EventViewModel _eventViewModel;
        private Event _selectedEvent;
        private string _uploadedImageUrl;

        public ModifyEventPage()
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();
            LoadEvents();
        }

        public ModifyEventPage(string eventId)
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();
            LoadEvents();
            LoadEventById(eventId);
        }

        private void LoadEvents()
        {
            var events = _eventViewModel.GetAllEvents();
            EventComboBox.ItemsSource = events;
        }

        private void LoadEventById(string eventId)
        {
            _selectedEvent = _eventViewModel.GetEventById(eventId);

            if (_selectedEvent == null)
            {
                MessageBox.Show("Event not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Auto-select in ComboBox
            EventComboBox.SelectedItem = EventComboBox.Items.Cast<Event>()
                .FirstOrDefault(e => e.Id == eventId);

            PopulateForm();
        }

        private void PopulateForm()
        {
            if (_selectedEvent == null) return;

            EventTitleTextBox.Text = _selectedEvent.Title;
            EventDatePicker.SelectedDate = _selectedEvent.EventDate;
            PriceTextBox.Text = _selectedEvent.Price;
            EventDescriptionTextBox.Text = _selectedEvent.Description;
            TagsTextBox.Text = string.Join(", ", _selectedEvent.Tags);
            FeaturedCheckBox.IsChecked = _selectedEvent.Featured;

            _uploadedImageUrl = _selectedEvent.ImageUrl;

            if (!string.IsNullOrEmpty(_selectedEvent.ImageUrl))
            {
                BitmapImage bitmap = new BitmapImage(new Uri(_selectedEvent.ImageUrl));
                PreviewImage.Source = bitmap;
                SelectedImageTextBlock.Text = System.IO.Path.GetFileName(_selectedEvent.ImageUrl);
            }
            else
            {
                PreviewImage.Source = null;
                SelectedImageTextBlock.Text = "No file selected";
            }
        }

        private void EventComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventComboBox.SelectedItem is Event ev)
            {
                _selectedEvent = ev;
                PopulateForm();
            }
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                SelectedImageTextBlock.Text = System.IO.Path.GetFileName(dialog.FileName);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(dialog.FileName);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                PreviewImage.Source = bitmap;

                try
                {
                    _uploadedImageUrl = Helpers.UploadToCloudinary(dialog.FileName, "Event");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Image upload failed: {ex.Message}",
                                    "Upload Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEvent == null)
            {
                MessageBox.Show("Please select an event first.", "Warning",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _selectedEvent.Title = EventTitleTextBox.Text.Trim();
            _selectedEvent.EventDate = EventDatePicker.SelectedDate ?? _selectedEvent.EventDate;
            _selectedEvent.Price = PriceTextBox.Text.Trim();
            _selectedEvent.Description = EventDescriptionTextBox.Text.Trim();
            _selectedEvent.Featured = FeaturedCheckBox.IsChecked == true;
            _selectedEvent.Tags = TagsTextBox.Text
                                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                    .ToList();
            _selectedEvent.ImageUrl = _uploadedImageUrl ?? _selectedEvent.ImageUrl;

            _eventViewModel.UpdateEvent(_selectedEvent);

            MessageBox.Show("Event updated successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            ClearForm();
            LoadEvents();
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEvent == null)
            {
                MessageBox.Show("Select an event to delete.", "Warning",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete \"{_selectedEvent.Title}\"?",
                "Delete Event",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                _eventViewModel.DeleteEvent(_selectedEvent.Id);

                MessageBox.Show("Event deleted successfully!", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                ClearForm();
                LoadEvents();
            }
        }

        private void ClearForm()
        {
            EventComboBox.SelectedIndex = -1;
            EventTitleTextBox.Text = "";
            EventDatePicker.SelectedDate = null;
            PriceTextBox.Text = "";
            TagsTextBox.Text = "";
            FeaturedCheckBox.IsChecked = false;
            EventDescriptionTextBox.Text = "";
            PreviewImage.Source = null;
            SelectedImageTextBlock.Text = "No file selected";

            _uploadedImageUrl = null;
            _selectedEvent = null;
        }
    }
}
