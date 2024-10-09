using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Task1QQQ.ViewModels;

namespace Task1QQQ.Views
{
    /// <summary>
    /// Interaction logic for AddSubstanceWindow.xaml
    /// </summary>
    public partial class AddSubstanceWindow : Window
    {
        public AddSubstanceWindow()
        {
            InitializeComponent();
            DataContext = new AddSubstanceViewModel();
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

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                textBox.SelectionStart = 1;
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
    }
}
