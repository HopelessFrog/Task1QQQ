using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Task1QQQ.Models;
using Task1QQQ.Views;

namespace Task1QQQ.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DbService _dbService = new();

        [ObservableProperty]
        private List<Substance> substances;


        public MainViewModel()
        {
            Substances = _dbService.GetSubstances();
        }

        [RelayCommand]
        private void AddSubstance()
        {
            var window = new AddSubstanceWindow();
            window.ShowDialog();
        }
    }
}
