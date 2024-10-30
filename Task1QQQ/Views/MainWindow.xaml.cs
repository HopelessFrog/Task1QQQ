using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Task1QQQ.ViewModels;

namespace Task1QQQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Regex regex = new Regex("[^0-9.]");

            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }

            if (e.Text == "." && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }


        }


        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                textBox.SelectionStart = 1;

            }
        }
    }
}