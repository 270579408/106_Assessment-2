using _106_Assessment_2.ViewModels;
using _106_Assessment_2.Models;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for Activities.xaml
    /// </summary>
    public partial class Activities : Page
    {
        private readonly ActivityViewModel _activityViewModel;

        public List<Activity> AllActivities { get; set; }

        public Activities()
        {
            InitializeComponent();

            _activityViewModel = new ActivityViewModel();
            AllActivities = _activityViewModel.GetAllActivities();

            DataContext = this;
        }
    }
}

