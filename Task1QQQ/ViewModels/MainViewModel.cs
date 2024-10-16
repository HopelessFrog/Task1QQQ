using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using Task1QQQ.Enums;
using Task1QQQ.Messages;
using Task1QQQ.Models;
using Task1QQQ.Views;

namespace Task1QQQ.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DbService _dbService = new();

        [ObservableProperty]
        private bool or;

        [ObservableProperty]
        private List<Substance> substances;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private decimal minDensity;
        [ObservableProperty]
        private decimal maxDensity;
        [ObservableProperty]
        private decimal minCalorificValue;
        [ObservableProperty]
        private decimal maxCalorificValue;

        private List<SubstanceType> substanceTypes;
        public List<SubstanceType> SubstanceTypes
        {
            get
            {
                var qwe = substanceTypes.ToList();
                qwe.Insert(0, new() { Id = 0, Name = "Не выбрано" });
                return qwe;
            }
            set
            {
                SetProperty(ref substanceTypes, value);
            }
        }

        [ObservableProperty]
        private List<Sorts> sortsVar;
        [ObservableProperty]
        private Sorts selectedSort;
        [ObservableProperty]
        private SubstanceType selectedSubstanceType;
        [ObservableProperty]
        private bool densityFilter = false;
        [ObservableProperty]
        private bool calorificValueFilter = false;


        public MainViewModel()
        {
            Substances = _dbService.FindSubstances();
            SubstanceTypes = _dbService.GetSubstanceTypes();
            WeakReferenceMessenger.Default.Register<SubstanceCreatedMessage>(this, (r, m) =>
            {
                Substances = m.Value;
            });

            WeakReferenceMessenger.Default.Register<SubstanceTypeCreatedMessage>(this, (r, m) =>
            {
                SubstanceTypes = m.Value;
            });

            SortsVar = new() { Sorts.None, Sorts.ByName, Sorts.ByDensity, Sorts.ByCalorificValue, Sorts.ByMinConcentration, Sorts.ByMaxConcentration };

            SelectedSubstanceType = SubstanceTypes[0];
        }

        [RelayCommand]
        private void Sort()
        {

            var qwe = SelectedSort.ToString();
            switch (SelectedSort)
            {
                case Sorts.None:
                    Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter);
                    break;
                case Sorts.ByName:
                    if (Or)
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderBy(s => s.Name).ToList(); ;
                    }
                    else
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderByDescending(s => s.Name).ToList(); ;

                    }
                    break;
                case Sorts.ByDensity:
                    if (Or)
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderBy(s => s.Density).ToList(); ;
                    }
                    else
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderByDescending(s => s.Density).ToList(); ;

                    }
                    break;
                case Sorts.ByCalorificValue:
                    if (Or)
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderBy(s => s.CalorificValue).ToList(); ;
                    }
                    else
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderByDescending(s => s.CalorificValue).ToList(); ;

                    }
                    break;
                case Sorts.ByMinConcentration:
                    if (Or)
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderBy(s => s.MinConcentration).ToList(); ;
                    }
                    else
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderByDescending(s => s.MinConcentration).ToList(); ;

                    }
                    break;
                case Sorts.ByMaxConcentration:
                    if (Or)
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderBy(s => s.MaxConcentration).ToList(); ;
                    }
                    else
                    {
                        Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter).OrderByDescending(s => s.MaxConcentration).ToList(); ;

                    }
                    break;
                default:
                    break;
            }
        }

        [RelayCommand]
        private void FindSubstance()
        {
            Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter);
        }

        [RelayCommand]
        private void Delete(Substance substance)
        {
            var result = MessageBox.Show($"Удалить {substance.Name}? ", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                _dbService.DeleteSubstance(substance.Id);
                Substances = _dbService.FindSubstances(name, minDensity, maxDensity, minCalorificValue, maxCalorificValue, SelectedSubstanceType?.Id, densityFilter, calorificValueFilter);
            }
        }

        [RelayCommand]
        private void Update(Substance substance)
        {
            var window = new AddSubstanceWindow(substance);
            window.ShowDialog();
        }

        [RelayCommand]
        private void AddSubstance()
        {
            var window = new AddSubstanceWindow();
            window.ShowDialog();
        }
    }
}
