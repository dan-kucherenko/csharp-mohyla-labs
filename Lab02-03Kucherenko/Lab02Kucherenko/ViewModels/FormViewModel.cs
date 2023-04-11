using KMA.Lab02.Kucherenko.Models;
using KMA.Lab02.Kucherenko.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.Lab02.Kucherenko.ViewModels
{
    internal class FormViewModel : INotifyPropertyChanged
    {
        #region Fields

        private bool _isEnabled = true;
        private RelayCommand<object> _proceedCommand;
        private Action _gotoResultView;

        public FormViewModel(Action gotoResultView)
        {
            _gotoResultView = gotoResultView;
        }

        #endregion

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
            PersonService ps = new PersonService();
            
            try
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    Person person = new Person(FirstName, LastName, Email, DateOfBirth);
                    if (person.IsAnyFieldNull())
                        return;
                    ps.SavePerson(person);
                    _gotoResultView?.Invoke();
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

        #region PropChangedImplementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}