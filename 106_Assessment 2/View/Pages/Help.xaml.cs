using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages
{
    public partial class Help : Page
    {
        public class FAQ
        {
            public string Question { set; get; }
            public string Answer { set; get; }
        }

        public List<FAQ> FAQs = new List<FAQ>()
        {
            new FAQ() { Question="How can I book tickets for the park?", Answer="You can book tickets directly through our website under the “Events & Bookings” section. Simply select your preferred date, choose the event or attraction, and complete the online payment process." },
            new FAQ() { Question="Do I need to create an account to book tickets?", Answer="Yes, you’ll need to create an account to manage your bookings, view your visit history, and receive event updates. Registration is quick and free." },
            new FAQ() { Question="Can I cancel or reschedule my booking?", Answer="Yes, bookings can be cancelled or rescheduled up to 24 hours before the scheduled visit. Log in to your account and navigate to “My Bookings” to make changes."},
            new FAQ() { Question="What payment methods do you accept?", Answer="We accept major credit and debit cards, including Visa, Mastercard, and American Express. For group bookings, please contact our support team for payment assistance." },
            new FAQ() { Question="Is the park accessible for wheelchairs and strollers?", Answer="Yes, most areas of the park, including the museum and café, are wheelchair and stroller accessible. Assistance can be requested at the entrance." },
            new FAQ() { Question="Can I bring food or drinks into the park?", Answer="Yes, visitors are welcome to bring food and non-alcoholic beverages. However, please use designated picnic areas and dispose of waste responsibly." },
            new FAQ() { Question="Are pets allowed in the park?", Answer="Pets are not allowed, except for certified service animals. This helps us protect the wildlife in the heritage park." },
        };

        public Help()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void InquiryInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            Placeholder_InquiryInput.Visibility = string.IsNullOrWhiteSpace(InquiryInput.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
