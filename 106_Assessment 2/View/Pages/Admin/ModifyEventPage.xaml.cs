using _106_Assessment_2.Common;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using Microsoft.Win32;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class ModifyEventPage : Page
    {
        private EventViewModel _eventViewModel;
        private Event _selectedEvent;
        private string _uploadedImageUrl;

        public ModifyEventPage()
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();
            LoadEvents();
        }

        private void LoadEvents()
        {
            var events = _eventViewModel.GetAllEvents();
            EventComboBox.ItemsSource = events;
        }

        private void EventComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventComboBox.SelectedItem is Event ev)
            {
                _selectedEvent = ev;

                // Populate fields
                EventTitleTextBox.Text = ev.Title;
                EventDatePicker.SelectedDate = ev.EventDate;
                PriceTextBox.Text = ev.Price;
                TagsTextBox.Text = string.Join(", ", ev.Tags);
                FeaturedCheckBox.IsChecked = ev.Featured;
                EventDescriptionTextBox.Text = ev.Description;

                _uploadedImageUrl = ev.ImageUrl;

                // Load preview image
                if (!string.IsNullOrEmpty(ev.ImageUrl))
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(ev.ImageUrl));
                    PreviewImage.Source = bitmap;
                    SelectedImageTextBlock.Text = System.IO.Path.GetFileName(ev.ImageUrl);
                }
                else
                {
                    PreviewImage.Source = null;
                    SelectedImageTextBlock.Text = "No file selected";
                }
            }
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImageTextBlock.Text = System.IO.Path.GetFileName(openFileDialog.FileName);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                PreviewImage.Source = bitmap;

                try
                {
                    // Upload to Cloudinary
                    _uploadedImageUrl = Helpers.UploadToCloudinary(openFileDialog.FileName, "Event");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Image upload failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEvent == null)
            {
                MessageBox.Show("Please select an event to modify.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update selected event
            _selectedEvent.Title = EventTitleTextBox.Text.Trim();
            _selectedEvent.EventDate = EventDatePicker.SelectedDate ?? _selectedEvent.EventDate;
            _selectedEvent.Price = PriceTextBox.Text.Trim();
            _selectedEvent.Tags = TagsTextBox.Text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
            _selectedEvent.Featured = FeaturedCheckBox.IsChecked == true;
            _selectedEvent.Description = EventDescriptionTextBox.Text.Trim();
            _selectedEvent.ImageUrl = _uploadedImageUrl ?? _selectedEvent.ImageUrl;

            _eventViewModel.UpdateEvent(_selectedEvent); // implement this in your EventViewModel

            MessageBox.Show("Event updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

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

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEvent == null)
            {
                MessageBox.Show("Please select an event to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete the event \"{_selectedEvent.Title}\"?",
                                         "Confirm Deletion",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _eventViewModel.DeleteEvent(_selectedEvent.Id);
                    MessageBox.Show("Event deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

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

                    LoadEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
