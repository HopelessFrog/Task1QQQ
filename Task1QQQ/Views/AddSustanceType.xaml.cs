using System.Windows;
using Task1QQQ.ViewModels;

namespace Task1QQQ.Views
{
    /// <summary>
    /// Interaction logic for AddSustanceType.xaml
    /// </summary>
    public partial class AddSustanceType : Window
    {
        public AddSustanceType()
        {
            InitializeComponent();
            DataContext = new AddSubstanceTypeViewModel();
            Loaded += AddSustanceType_Loaded;
        }

        private void AddSustanceType_Loaded(object sender, RoutedEventArgs e)
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
