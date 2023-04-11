using KMA.Lab02.Kucherenko.Models;
using System;
using KMA.Lab02.Kucherenko.Tools;
using KMA.Lab02.Kucherenko.Tools.Signs;

namespace KMA.Lab02.Kucherenko.ViewModels
{
    internal class ResultViewModel
    {
        #region Fields

        private Action _gotoFormView;
        private RelayCommand<object> _returnCommand;
        private readonly PersonService _personService;

        #endregion

        public ResultViewModel(Action gotoFormView)
        {
            _gotoFormView = gotoFormView;
            _personService = new PersonService();
        }

        #region Properties

        public Person Person => _personService.Person;

        public string FirstName => Person.FirstName;

        public string LastName => Person.LastName;

        public string Email => Person.Email;

        public DateTime DateOfBirth => Person.DateOfBirth;

        public bool IsAdult => Person.IsAdult;

        public SunSign SunSign => Person.SunSign;

        public ChineseSign ChineseSign => Person.ChineseSign;

        public bool IsBirthday => Person.IsBirthday;

        public RelayCommand<object> ReturnCommand => _returnCommand ??= new RelayCommand<object>(_ => ReturnToForm());

        #endregion

        public void ReturnToForm()
        {
            _gotoFormView?.Invoke();
        }
    }
}