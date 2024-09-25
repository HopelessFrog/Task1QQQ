using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Task1QQQ.ViewModels
{
    public partial class AddSubstanceTypeViewModel : ObservableValidator, ICloseWindow
    {
        private DbService _dbService = new();

        [MinLength(5, ErrorMessage = "Login must be at least 5 characters long.")]
        [MaxLength(45, ErrorMessage = "Login cannot be longer than 20 characters.")]
        [ObservableProperty]
        private string name;
        public Action Close { get; set; }

        public bool CanClose()
        {
            return true;
        }

        [RelayCommand]
        private void AddSubstanceType()
        {
            if (_dbService.AddSubstanceType(name))
            {
                MessageBox.Show("Тип вещества добавлен");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        [RelayCommand]
        private void Cancel()
        {
            Close?.Invoke();
        }
    }
}
