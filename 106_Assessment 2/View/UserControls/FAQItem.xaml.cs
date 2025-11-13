using System.Windows;
using System.Windows.Controls;

namespace _106_Assessment_2.View.UserControls
{
    public partial class FAQItem : UserControl
    {
        public FAQItem()
        {
            InitializeComponent();
        }

        // Dependency property for Question
        public static readonly DependencyProperty QuestionProperty =
            DependencyProperty.Register(
                nameof(Question),
                typeof(string),
                typeof(FAQItem),
                new PropertyMetadata(string.Empty));

        public string Question
        {
            get => (string)GetValue(QuestionProperty);
            set => SetValue(QuestionProperty, value);
        }

        // Dependency property for Answer
        public static readonly DependencyProperty AnswerProperty =
            DependencyProperty.Register(
                nameof(Answer),
                typeof(string),
                typeof(FAQItem),
                new PropertyMetadata(string.Empty));

        public string Answer
        {
            get => (string)GetValue(AnswerProperty);
            set => SetValue(AnswerProperty, value);
        }

        // 👇 This handles the button click to show/hide the answer
        //private void FAQ_Button(object sender, RoutedEventArgs e)
        //{
        //    // Toggle between visible and collapsed
        //    FAQAnswer.Visibility = FAQAnswer.Visibility == Visibility.Visible
        //        ? Visibility.Collapsed
        //        : Visibility.Visible;
        //}
    }
}
