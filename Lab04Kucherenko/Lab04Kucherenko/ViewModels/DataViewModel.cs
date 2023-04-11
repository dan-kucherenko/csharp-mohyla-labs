using KMA.Lab04.Kucherenko.Models;
using KMA.Lab04.Kucherenko.Navigation;
using KMA.Lab04.Kucherenko.Repositories;
using KMA.Lab04.Kucherenko.Services;
using KMA.Lab04.Kucherenko.Tools;
using KMA.Lab04.Kucherenko.Tools.Signs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace KMA.Lab04.Kucherenko.ViewModels
{
    internal class DataViewModel : INavigatable, INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<SavedPerson> _persons;
        private Action _gotoFormView;
        private Action _gotoUpdateFormView;
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _applyFilters;
        private RelayCommand<object> _cancelFilters;
        private string _sorterName;

        #endregion

        public DataViewModel(Action gotoFormView, Action gotoUpdateFormView)
        {
            _gotoFormView = gotoFormView;
            _gotoUpdateFormView = gotoUpdateFormView;
            _persons = new ObservableCollection<SavedPerson>(new PersonService().GetAllPersons());
            new FileRepository().AddInitialPersons();
        }

        #region Properties

        public ObservableCollection<SavedPerson> Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                NotifyPropertyChanged();
            }
        }

        public static SavedPerson SelectedPerson { get; set; }

        public static List<string> SortingFields { get; } = new List<string>
        {
            "First name", "Last name", "Email", "Date of birth", "IsBirthday", "IsAdult", "Sun sign", "Chinese sign",
            "No sorting"
        };

        public string SortingBy
        {
            get => _sorterName;
            set
            {
                _sorterName = value;
                PerformSorting();
            }
        }

        public bool IsAdultChecked { get; set; }

        public bool IsBirthdayChecked { get; set; }

        public static List<string> SunSignsString { get; } = new List<string>
        {
            "Aries",
            "Taurus",
            "Gemini",
            "Cancer",
            "Leo",
            "Virgo",
            "Libra",
            "Scorpio",
            "Sagittarius",
            "Capricorn",
            "Aquarius",
            "Pisces",
            "Default"
        };

        public static List<string> ChineseSignsString { get; } = new List<string>
        {
            "Monkey",
            "Rooster",
            "Dog",
            "Pig",
            "Rat",
            "Ox",
            "Tiger",
            "Rabbit",
            "Dragon",
            "Snake",
            "Horse",
            "Goat",
            "Default"
        };

        public RelayCommand<object> ApplyFilters => _applyFilters ??= new RelayCommand<object>(_ => FilterPersons());

        public RelayCommand<object> CancelFilters => _cancelFilters ??= new RelayCommand<object>(_ =>
        {
            Persons = new ObservableCollection<SavedPerson>(new PersonService().GetAllPersons());
        });

        public string SunSignSelected { get; set; }

        public string ChineseSignSelected { get; set; }

        #endregion

        #region Commands

        public RelayCommand<object> AddPersonCommand =>
            _addCommand ??= new RelayCommand<object>(_ => AddPerson());

        public RelayCommand<object> EditPersonCommand =>
            _editCommand ??= new RelayCommand<object>(_ => EditPerson());

        public RelayCommand<object> DeletePersonCommand =>
            _deleteCommand ??= new RelayCommand<object>(_ => DeletePerson());

        #endregion

        #region Commands' Methods

        private void AddPerson()
        {
            _gotoFormView?.Invoke();
        }

        private void EditPerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("You should select a person to edit");
                return;
            }
            _gotoUpdateFormView?.Invoke();
        }

        private void DeletePerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("You should select a person to delete");
                return;
            }

            new DeletePersonService().DeletePerson(SelectedPerson);
            Persons = new ObservableCollection<SavedPerson>(new PersonService().GetAllPersons());
        }

        private void PerformSorting()
        {
            switch (_sorterName)
            {
                case "First Name":
                    Persons = new ObservableCollection<SavedPerson>(from person in _persons
                        orderby person.FirstName
                        select person);
                    break;
                case "Last name":
                    Persons = new ObservableCollection<SavedPerson>(from person in _persons
                        orderby person.LastName
                        select person);
                    break;
                case "Email":
                    Persons = new ObservableCollection<SavedPerson>(from person in _persons
                        orderby person.Email
                        select person);
                    break;
                case "Date of birth":
                    Persons = new ObservableCollection<SavedPerson>(from person in _persons
                        orderby person.DateOfBirth
                        select person);
                    break;
                case "IsBirthday":
                    Persons = new ObservableCollection<SavedPerson>(from person in _persons
                        orderby person.IsBirthday
                        select person);
                    break;
                case "IsAdult":
                    Persons = new ObservableCollection<SavedPerson>(from person in _persons
                        orderby person.IsAdult
                        select person);
                    break;
                case "Sun sign":
                    Persons = new ObservableCollection<SavedPerson>(from person in _persons
                        orderby person.SunSign
                        select person);
                    break;
                case "Chinese sign":
                    Persons = new ObservableCollection<SavedPerson>(from person in _persons
                        orderby person.ChineseSign
                        select person);
                    break;
                default:
                    Persons = new ObservableCollection<SavedPerson>(new PersonService().GetAllPersons());
                    break;
            }
        }

        private void FilterPersons()
        {
            Persons = new ObservableCollection<SavedPerson>(new PersonService().GetAllPersons());
            if (IsAdultChecked)
            {
                Persons = new ObservableCollection<SavedPerson>(
                    from person in _persons
                    where person.IsAdult
                    select person);
            }

            if (IsBirthdayChecked)
            {
                Persons = new ObservableCollection<SavedPerson>(
                    from person in _persons
                    where person.IsBirthday
                    select person);
            }

            if (!String.IsNullOrEmpty(SunSignSelected) && !SunSignSelected.Equals("Default"))
            {
                Persons = new ObservableCollection<SavedPerson>(
                    from person in _persons
                    where person.SunSign == (SunSign)Enum.Parse(typeof(SunSign), SunSignSelected)
                    select person);
            }

            if (!String.IsNullOrEmpty(ChineseSignSelected) && !ChineseSignSelected.Equals("Default"))
            {
                Persons = new ObservableCollection<SavedPerson>(
                    from person in _persons
                    where person.ChineseSign == (ChineseSign)Enum.Parse(typeof(ChineseSign), ChineseSignSelected)
                    select person);
            }
        }

        #endregion

        public NavigationViewTypes ViewType => NavigationViewTypes.DataViewModel;

        #region PropChangedImplementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}