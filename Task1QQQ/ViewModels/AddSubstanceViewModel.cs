﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Task1QQQ.Messages;
using Task1QQQ.Models;
using Task1QQQ.Views;

namespace Task1QQQ.ViewModels
{
    public partial class AddSubstanceViewModel : ObservableValidator, ICloseWindow
    {
        private DbService _dbService = new();

        [ObservableProperty]
        private List<SubstanceType> substanceTypes;

        [Required(ErrorMessage = "Укажите тип")]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private SubstanceType selectedSubstanceType;

        [Required(ErrorMessage = "Укажите название")]
        [MinLength(5, ErrorMessage = "Имя дожно быть длиннее 5")]
        [MaxLength(45, ErrorMessage = "Имя должно быть короче 45")]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string name;

        [Range(0, 99.999, ErrorMessage = "Значение должно быть UNSIGNED DECIMAL(5,3)")]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private decimal destiny;

        [Range(0, 99.999, ErrorMessage = "Значение должно быть UNSIGNED DECIMAL(5,3)")]
        [ObservableProperty]
        private decimal calorificValue;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(0, 255, ErrorMessage = "Значение должно быть UNSIGNED TINYINT")]
        private int minConcetration;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(0, 255, ErrorMessage = "Значение должно быть UNSIGNED TINYINT")]
        private int maxConcetration;
        public Action Close { get; set; }

        public AddSubstanceViewModel()
        {
            SubstanceTypes = _dbService.GetSubstanceTypes();
            WeakReferenceMessenger.Default.Register<SubstanceTypeCreatedMessage>(this, (r, m) =>
            {
                SubstanceTypes = m.Value;
            });
        }

        public bool CanClose()
        {
            return true;
        }


        [RelayCommand]
        private void AddSubstanceType()
        {
            var window = new AddSustanceType();
            window.ShowDialog();
        }

        [RelayCommand]
        private void AddSubstance()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                return;
            }
            try
            {
                _dbService.AddSubstance(name, Destiny, CalorificValue, minConcetration, maxConcetration, SelectedSubstanceType.Id);
                MessageBox.Show("Вещество добавлено");
                WeakReferenceMessenger.Default.Send(new SubstanceCreatedMessage(_dbService.FindSubstances()));

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
