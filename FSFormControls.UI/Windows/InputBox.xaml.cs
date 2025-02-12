using System;
using System.Windows;
using System.Windows.Media;

namespace FSFormControls.UI.Windows
{
    /// <summary>
    /// Lógica de interacción para InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        public event Action OnYesButton;
        public event Action OnNoButton;

        public InputBox()
        {
            InitializeComponent();
        }

        public new Visibility Visibility
        {
            get { return InputBoxGrid.Visibility; }
            set { InputBoxGrid.Visibility = value; }
        }

        public string Header
        {
            get { return TextBoxHeader.Text; }
            set { TextBoxHeader.Text = value; }
        }

        public Brush HeaderColor
        {
            get { return TextBoxHeader.Foreground; }
            set { TextBoxHeader.Foreground = value; }
        }

        public string Text
        {
            get { return InputTextBox.Text; }
            set { InputTextBox.Text = value; }
        }

        public Brush Color
        {
            get { return borderBackground.Background; }
            set { borderBackground.Background = value; }
        }

        public string YesButtonText
        {
            get { return YesButton.Content.ToString(); }
            set { YesButton.Content = value; }
        }

        public string NoButtonText
        {
            get { return NoButton.Content.ToString(); }
            set { NoButton.Content = value; }
        }

        public string Result
        {
            get { return InputTextBox.Text; }
        }

        private void NoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OnNoButton?.Invoke();
        }

        private void YesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OnYesButton?.Invoke();
        }
    }
}
