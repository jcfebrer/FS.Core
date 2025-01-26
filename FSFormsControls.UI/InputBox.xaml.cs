using System;
using System.Windows;
using System.Windows.Controls;

namespace FSFormsControls.UI
{
    /// <summary>
    /// Lógica de interacción para InputBox.xaml
    /// </summary>
    public partial class InputBox : UserControl
    {
        public event Action OnYesButton;
        public event Action OnNoButton;

        public InputBox()
        {
            InitializeComponent();

            InputBoxGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void Show()
        {
            InputBoxGrid.Visibility = System.Windows.Visibility.Visible;
        }

        public void Close()
        {
            InputBoxGrid.Visibility = System.Windows.Visibility.Collapsed;
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
