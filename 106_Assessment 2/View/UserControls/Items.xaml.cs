using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.UserControls
{
    public partial class Items : UserControl
    {
        public Items()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty ItemIconProperty =
            DependencyProperty.Register(
                "ItemIcon",
                typeof(string),
                typeof(Items),
                new PropertyMetadata(string.Empty));

        public string ItemIcon
        {
            get => (string)GetValue(ItemIconProperty);
            set => SetValue(ItemIconProperty, value);
        }

        public static readonly DependencyProperty ItemTitleProperty =
            DependencyProperty.Register(
                "ItemTitle",
                typeof(string),
                typeof(Items),
                new PropertyMetadata(string.Empty));

        public string ItemTitle
        {
            get => (string)GetValue(ItemTitleProperty);
            set => SetValue(ItemTitleProperty, value);
        }
    }
}