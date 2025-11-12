using _106_Assessment_2.ViewModels;
using _106_Assessment_2.Models;
using System.Windows.Controls;

namespace _106_Assessment_2.View.Pages
{
    /// <summary>
    /// Interaction logic for ThingsToDo.xaml
    /// </summary>
    public partial class ThingsToDo : Page
    {
        private readonly ThingsToDoViewModel _thingstodoViewModel;

        public List<ThingsToDo>Activities { get; set; }

        public ThingsToDo()
        {
            InitializedComponent();

            _thingstodoViewModel = new ThingsToDoViewModel();
            Activities = _thingstodoViewModel.GetAllActivities();
            DataContext = this;
        }
    }
}

