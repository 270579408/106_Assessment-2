using _106_Assessment_2.Models;
using _106_Assessment_2.View.Pages;
using _106_Assessment_2.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace _106_Assessment_2.View.Pages
{
    public partial class BookEvent : Page
    {
        private decimal EventPrice;
        private decimal DiscountPercent = 5m;
        private bool DiscountApplied = false;

        private BookingViewModel _booking;

        public Event EventInfo { get; set; }

        public BookEvent(Event selectedEvent)
        {
            InitializeComponent();

            _booking = new BookingViewModel();

            EventInfo = selectedEvent;

            FeaturedEventsTitle.Text = EventInfo.Title;
            FeaturedEventsDescription.Text = EventInfo.Description;
            EventDateTxt.Text = "Date: " + EventInfo.EventDate.ToString("dd MMM yyyy");
            EventLocationTxt.Text = "Location: Onewhero Bay Park";
            EventPriceTxt.Text = "Price per ticket: " + EventInfo.Price;

            EventPrice = Convert.ToDecimal(EventInfo.Price);

            EventDate.Text = EventInfo.EventDate.ToString("dd MMM yyyy");
            FeaturedEventsBannerImg.Source = new BitmapImage(new Uri(EventInfo.ImageUrl, UriKind.Absolute));
            EventFullDescriptionTxt.Text = EventInfo.Description;

            TicketCountCombo.SelectedIndex = 0;
            UpdateTotalPrice();

            PromotionBanner.Visibility = !(GlobalData.CurrentUserId == null &&
                GlobalData.CurrentUserEmail == null &&
                GlobalData.CurrentUserName == null) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void TicketCountCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            if (TicketCountCombo.SelectedItem is ComboBoxItem item &&
                int.TryParse(item.Content.ToString(), out int count))
            {
                decimal total = EventPrice * count;

                DiscountApplied = false;

                if (!(GlobalData.CurrentUserId == null &&
                GlobalData.CurrentUserEmail == null &&
                GlobalData.CurrentUserName == null))
                {
                    total -= total * (DiscountPercent / 100);
                    DiscountApplied = true;
                    TotalPriceTxt.Text = $"Total Price (5% discount applied): ${total:F2}";
                }
                else
                {
                    TotalPriceTxt.Text = $"Total Price: ${total:F2}";
                }
            }
        }

        private void PromotionBanner_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new Register());
        }

        private void ConfirmBooking_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTxt.Text) ||
                string.IsNullOrWhiteSpace(EmailTxt.Text) ||
                TicketCountCombo.SelectedIndex < 0)
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PaymentSection.Visibility = Visibility.Visible;

            MessageBox.Show("Booking details confirmed! Please proceed to payment.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PayNow_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CardHolderTxt.Text) ||
                string.IsNullOrWhiteSpace(CardNumberTxt.Text) ||
                string.IsNullOrWhiteSpace(ExpiryTxt.Text) ||
                string.IsNullOrWhiteSpace(CVVTxt.Text))
            {
                MessageBox.Show("Please fill in all payment fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal amount = GetFinalPrice();

            System.Threading.Thread.Sleep(500);

            MessageBox.Show($"Payment successful! Total paid: ${amount:F2}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            Booking b = new Booking()
            {
                EventId = EventInfo.Id,
                UserId = GlobalData.CurrentUserId != null ? GlobalData.CurrentUserId : string.Empty,
                BookingName = FullNameTxt.Text,
                BookingEmail = EmailTxt.Text,
                BookingPhoneNumber = string.IsNullOrWhiteSpace(PhoneTxt.Text) ? string.Empty : PhoneTxt.Text,
                BookingSpecialReq = string.IsNullOrWhiteSpace(SpecialReqTxt.Text) ? string.Empty : SpecialReqTxt.Text,
                TicketCount = TicketCountCombo.SelectedIndex,
                TotalPrice = amount,
                BookingDate = DateTime.Now,
                EventDate = EventInfo.EventDate
            };

            _booking.AddBooking(b);

            ClearInputFields();

            PaymentSection.Visibility = Visibility.Hidden;
        }

        private decimal GetFinalPrice()
        {
            int count = 1;
            if (TicketCountCombo.SelectedItem is ComboBoxItem item &&
                int.TryParse(item.Content.ToString(), out int c))
            {
                count = c;
            }

            decimal total = EventPrice * count;
            if (!(GlobalData.CurrentUserId == null &&
                GlobalData.CurrentUserEmail == null &&
                GlobalData.CurrentUserName == null))
            {
                total -= total * (DiscountPercent / 100);
            }
            return total;
        }

        private void ClearInputFields()
        {
            FullNameTxt.Text = string.Empty;
            EmailTxt.Text = string.Empty;
            PhoneTxt.Text = string.Empty;
            TicketCountCombo.SelectedIndex = 0;
            SpecialReqTxt.Text = string.Empty;

            CardHolderTxt.Text = string.Empty;
            CardNumberTxt.Text = string.Empty;
            ExpiryTxt.Text = string.Empty;
            CVVTxt.Text = string.Empty;

            UpdateTotalPrice();
        }
    }
}
