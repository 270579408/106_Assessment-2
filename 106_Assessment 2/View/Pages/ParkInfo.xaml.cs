using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages
{
    public partial class ParkInfo : Page
    {
        public class KeyFeatureItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string ImagePath { get; set; }
        }

        public List<KeyFeatureItem> KeyFeatures { get; set; } = new List<KeyFeatureItem>
        {
            new KeyFeatureItem { Title = "Native Wildlife Encounter", Description = "Meet some of New Zealand’s rarest species — from the iconic kiwi bird to the ancient tuatara. Learn about our conservation efforts and see native animals up close in their natural habitats.", ImagePath = "/Resources/key-features-1.png" },
            new KeyFeatureItem { Title = "Nocturnal Kiwi House", Description = "Step into the darkness and experience the life of New Zealand’s national bird. The Kiwi House offers a rare chance to observe these shy creatures in a natural night-time environment.", ImagePath = "/Resources/key-features-2.png" },
            new KeyFeatureItem { Title = "Heritage Museum", Description = "Explore the stories of Onewhero’s past through fascinating displays, historical artifacts, and exhibits that celebrate both Māori and European heritage.", ImagePath = "/Resources/key-features-3.png" },
            new KeyFeatureItem { Title = "Māori Marae Experience", Description = "Gain insight into the rich traditions of the local iwi. Visitors are welcomed to learn about Māori customs, arts, and history through guided cultural experiences.", ImagePath = "/Resources/key-features-4.png" },
        };

        public ParkInfo()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
