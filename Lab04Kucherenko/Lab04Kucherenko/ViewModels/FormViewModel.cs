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
    internal class FormViewModel : INotifyPropertyChanged, INavigatable
    {
        #region Fields

        private bool _isEnabled = true;
        private RelayCommand<object> _proceedCommand;
        private Action _gotoDataView;

        #endregion

        public FormViewModel(Action gotoDataView)
        {
            _gotoDataView = gotoDataView;
        }


        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.Today;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand<object> ProceedCommand =>
            _proceedCommand ??= new RelayCommand<object>(_ => Proceed(), BoxesFilled);

        #endregion

        internal async void Proceed()
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
                    MessageBox.Show("Person has been successfully added");
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

        private bool BoxesFilled(object obj)
        {
            return !String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName) &&
                   !String.IsNullOrWhiteSpace(Email);
        }

        public NavigationViewTypes ViewType => NavigationViewTypes.FormViewModel;

        #region PropChangedImplementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}