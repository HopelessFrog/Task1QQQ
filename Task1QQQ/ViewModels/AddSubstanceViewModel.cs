using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Task1QQQ.Models;
using Task1QQQ.Views;

namespace Task1QQQ.ViewModels
{
    public partial class AddSubstanceViewModel : ObservableObject, ICloseWindow
    {
        private DbService _dbService = new();

        [ObservableProperty]
        private List<SubstanceType> substanceTypes;
        public Action Close { get; set; }

        public bool CanClose()
        {
            return true;
        }

        public AddSubstanceViewModel()
        {
            SubstanceTypes = _dbService.GetSubstanceTypes();
        }

        [RelayCommand]
        private void AddSubstanceType()
        {
            var window = new AddSustanceType();
            window.ShowDialog();
        }
    }
}
