using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.UserControls
{
    public partial class ActivityItem : UserControl
    {
        public ActivityItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ActivityItemIconProperty = 
            DependencyProperty.Register(
                "ActivityItemIcon",
                typeof(string),
                typeof(ActivityItem),
                new PropertyMetadata(string.Empty));

        public string ActivityItemIcon
        {
            get => (string)GetValue(ActivityItemIconProperty);
            set => SetValue(ActivityItemIconProperty, value);
        }

        public static readonly DependencyProperty ActivityItemTitleProperty =
            DependencyProperty.Register(
                "ActivityItemTitle",
                typeof(string),
                typeof(ActivityItem),
                new PropertyMetadata(string.Empty));

        public string ActivityItemTitle
        {
            get => (string)GetValue(ActivityItemTitleProperty);
            set => SetValue(ActivityItemTitleProperty, value);
        }
    }
}
