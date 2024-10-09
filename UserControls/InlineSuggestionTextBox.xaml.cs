using System;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.UserControls
{
    public partial class InlineSuggestionTextBox : UserControl
    {
        public InlineSuggestionTextBox()
        {
            InitializeComponent();
            InputTextBox.GotFocus += InputTextBox_GotFocus;
            InputTextBox.LostFocus += InputTextBox_LostFocus;
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(InlineSuggestionTextBox), new PropertyMetadata(string.Empty, OnTextChanged));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as InlineSuggestionTextBox;
            control.UpdateSuggestion();
        }

        public static readonly DependencyProperty SuggestionProperty =
            DependencyProperty.Register("Suggestion", typeof(string), typeof(InlineSuggestionTextBox), new PropertyMetadata(string.Empty, OnSuggestionChanged));

        public string Suggestion
        {
            get => (string)GetValue(SuggestionProperty);
            set => SetValue(SuggestionProperty, value);
        }

        private static void OnSuggestionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as InlineSuggestionTextBox;
            control.UpdateSuggestion();
        }

        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateSuggestion();
        }

        private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SuggestionTextBlock.Visibility = Visibility.Collapsed;
        }

        private void UpdateSuggestion()
        {
            if (InputTextBox.IsFocused && !string.IsNullOrEmpty(Text) && Suggestion.StartsWith(Text, StringComparison.OrdinalIgnoreCase))
            {
                SuggestionTextBlock.Text = Suggestion;
                SuggestionTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                SuggestionTextBlock.Visibility = Visibility.Collapsed;
            }
        }
    }
}