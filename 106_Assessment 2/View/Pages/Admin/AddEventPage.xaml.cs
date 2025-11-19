using _106_Assessment_2.Common;
using _106_Assessment_2.Models;
using _106_Assessment_2.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _106_Assessment_2.View.Pages.Admin
{
    public partial class AddEventPage : Page
    {
        private EventViewModel _eventViewModel;
        private string _uploadedImageUrl; // stores Cloudinary URL

        public AddEventPage()
        {
            InitializeComponent();
            _eventViewModel = new EventViewModel();
        }

        // -----------------------------
        // Select Image Button
        // -----------------------------
        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImageTextBlock.Text = System.IO.Path.GetFileName(openFileDialog.FileName);

                // Preview the image
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

        // -----------------------------
        // Save Event Button
        // -----------------------------
        private void SaveEventButton_Click(object sender, RoutedEventArgs e)
        {
            string title = EventTitleTextBox.Text.Trim();
            DateTime? eventDate = EventDatePicker.SelectedDate;
            string description = EventDescriptionTextBox.Text.Trim();
            string price = PriceTextBox.Text.Trim();
            string tagsText = TagsTextBox.Text.Trim();

            if (string.IsNullOrEmpty(title) || eventDate == null)
            {
                MessageBox.Show("Please enter both title and date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<string> tags = new List<string>();
            if (!string.IsNullOrEmpty(tagsText))
            {
                tags = new List<string>(tagsText.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            }

            var newEvent = new Event
            {
                Title = title,
                EventDate = eventDate.Value,
                Description = description,
                Price = price,
                Tags = tags,
                ImageUrl = _uploadedImageUrl ?? "",
                Featured = FeaturedCheckBox.IsChecked == true, 
                RegisteredUserIds = new List<string>(),
                CreatedAt = DateTime.Now
            };

            _eventViewModel.AddEvent(newEvent);

            MessageBox.Show("Event added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear form
            EventTitleTextBox.Text = "";
            EventDatePicker.SelectedDate = null;
            EventDescriptionTextBox.Text = "";
            PriceTextBox.Text = "";
            TagsTextBox.Text = "";
            _uploadedImageUrl = null;
            SelectedImageTextBlock.Text = "No file selected";
            PreviewImage.Source = null;
        }
    }
}