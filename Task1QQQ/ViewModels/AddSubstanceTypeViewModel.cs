using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Task1QQQ.Messages;

namespace Task1QQQ.ViewModels
{
    public partial class AddSubstanceTypeViewModel : ObservableValidator, ICloseWindow
    {
        private DbService _dbService = new();

        [MinLength(5, ErrorMessage = "Имя дожно ыбть длиннее 5")]
        [MaxLength(45, ErrorMessage = "Имя должно быть короче 45")]
        [NotifyDataErrorInfo]
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
            if (HasErrors)
            {
                return;
            }
            try
            {
                _dbService.AddSubstanceType(name);
                MessageBox.Show("Тип вещества добавлен");
                WeakReferenceMessenger.Default.Send(new SubstanceTypeCreatedMessage(_dbService.GetSubstanceTypes()));

                Close?.Invoke();

            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("Тип вещества c таким именем уже существует");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неизвестная ошибка");
            }

        }

        [RelayCommand]
        private void Cancel()
        {
            Close?.Invoke();
        }
    }
}
