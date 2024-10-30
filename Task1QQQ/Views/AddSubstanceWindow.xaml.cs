using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Task1QQQ.Models;
using Task1QQQ.ViewModels;

namespace Task1QQQ.Views
{
    /// <summary>
    /// Interaction logic for AddSubstanceWindow.xaml
    /// </summary>
    public partial class AddSubstanceWindow : Window
    {

        public AddSubstanceWindow(Substance substance = null)
        {
            InitializeComponent();
            DataContext = new AddSubstanceViewModel(substance);
            Loaded += AddSubstanceWindow_Loaded;
        }

        private void AddSubstanceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindow viewModel)
            {
                viewModel.Close +=
                    () => { this.Close(); };

                Closing += (s, e) => { e.Cancel = !viewModel.CanClose(); };
            }
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

        private void TextBox_PreviewTextInput_1(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");

            if (regex.IsMatch(e.Text))
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
