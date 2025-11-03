using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.UserControls
{
    /// <summary>
    /// Interaction logic for SidebarBtn.xaml
    /// </summary>
    public partial class SidebarBtn : UserControl
    {
        public SidebarBtn()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PageNameProperty =
            DependencyProperty.Register(
                "PageName",
                typeof(string),
                typeof(SidebarBtn),
                new PropertyMetadata(string.Empty));

        public string PageName
        {
            get => (string)GetValue(PageNameProperty);
            set => SetValue(PageNameProperty, value);
        }

        public static readonly DependencyProperty PageIconProperty =
            DependencyProperty.Register(
                "PageIcon",
                typeof(string),
                typeof(SidebarBtn),
                new PropertyMetadata(string.Empty));

        public string PageIcon
        {
            get => (string)GetValue(PageIconProperty);
            set => SetValue(PageIconProperty, value);
        }
    }
}
