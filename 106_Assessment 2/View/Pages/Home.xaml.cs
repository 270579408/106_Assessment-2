using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

class Banner
{
    public string Id;
    public string ImageUrl;
    public string Title;
    public string Description;
    public string EventDate;
    public string CurrentDate;
    public string tag;
}

namespace _106_Assessment_2.View.Pages
{
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

            List<Banner> banners = new List<Banner>()
            {
                new Banner
                {
                    Id = "1",
                    ImageUrl = "/Resources/event1.jpg",
                    Title = "Summer Festival",
                    Description = "Join us for a day of fun, food, and festivities at the annual Summer Festival!",
                    EventDate = "2024-07-15",
                    CurrentDate = "2024-06-01",
                    tag = "Festival"
                },
                new Banner
                {
                    Id = "2",
                    ImageUrl = "/Resources/event2.jpg",
                    Title = "Art in the Park",
                    Description = "Experience the beauty of local art and creativity at our Art in the Park event.",
                    EventDate = "2024-08-10",
                    CurrentDate = "2024-06-01",
                    tag = "Art"
                },
                new Banner
                {
                    Id = "3",
                    ImageUrl = "/Resources/event3.jpg",
                    Title = "Music Under the Stars",
                    Description = "Enjoy an evening of live music performances under the night sky.",
                    EventDate = "2024-09-05",
                    CurrentDate = "2024-06-01",
                    tag = "Music"
                },
                new Banner
                {
                    Id = "4",
                    ImageUrl = "/Resources/event4.jpg",
                    Title = "Food Truck Fiesta",
                    Description = "Savor delicious dishes from a variety of food trucks at our Food Truck Fiesta.",
                    EventDate = "2024-07-25",
                    CurrentDate = "2024-06-01",
                    tag = "Food"
                }
            };

            FeaturedEventsBannerImg.Source = new BitmapImage(new Uri(banners[0].ImageUrl, UriKind.Relative));
            FeaturedEventsTitle.Text = banners[0].Title;
            FeaturedEventsDescription.Text = banners[0].Description;
        }
    }
}
