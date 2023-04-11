using KMA.Lab04.Kucherenko.Models;
using KMA.Lab04.Kucherenko.Navigation;
using KMA.Lab04.Kucherenko.Services;
using KMA.Lab04.Kucherenko.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.Lab04.Kucherenko.ViewModels
{
    internal class UpdateFormViewModel : INotifyPropertyChanged, INavigatable
    {
        private RelayCommand<object> _updateCommand;
        private bool _isEnabled = true;
        private Action _gotoDataView;

        public UpdateFormViewModel(Action gotoDataView)
        {
            _gotoDataView = gotoDataView;
        }

        #region Properties

        public string FirstName { get; set; } = DataViewModel.SelectedPerson.FirstName;
        public string LastName { get; set; } = DataViewModel.SelectedPerson.LastName;
        public string Email { get; set; } = DataViewModel.SelectedPerson.Email;
        public DateTime DateOfBirth { get; set; } = DataViewModel.SelectedPerson.DateOfBirth;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand<object> UpdateCommand => _updateCommand ??= new RelayCommand<object>(_ => UpdatePerson());

        #endregion

        private async void UpdatePerson()
        {
            IsEnabled = false;
            try
            {
                await Task.Run(async () =>
                {
                    Person person = new Person(FirstName, LastName, Email, DateOfBirth);
                    var addPersonService = new AddPersonService();
                    if (addPersonService.IsAnyFieldNull(person))
                        return;
                    await addPersonService.AddPerson(person);
                    MessageBox.Show("Person has been successfully updated");
                    _gotoDataView.Invoke();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsEnabled = true;
            }
        }

        public NavigationViewTypes ViewType => NavigationViewTypes.UpdateFormViewModel;

        #region PropChangedImplementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}