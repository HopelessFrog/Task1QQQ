using System.Windows;
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
    }
}
