using KMA.Lab01Kucherenko.Models;
using KMA.Lab01Kucherenko.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace KMA.Lab01Kucherenko.ViewModels
{
    internal class ZodiacViewModel : INotifyPropertyChanged
    {
        #region Fields

        private User _user = new User();
        private RelayCommand<object> _signsRequested;

        #endregion

        #region Properties

        public int Age
        {
            get => _user.Age;
            private set
            {
                _user.Age = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get => _user.DateOfBirth;
            set
            {
                if (_user.DateOfBirth != value)
                {
                    _user.DateOfBirth = value;
                    DateChanged();
                    NotifyPropertyChanged();
                }
            }
        }

        public ZodiacSigns Zodiac
        {
            get => _user.Zodiac;
            private set
            {
                _user.Zodiac = value;
                NotifyPropertyChanged();
            }
        }

        public ChineseZodiacSigns ChineseZodiac
        {
            get => _user.ChineseZodiac;
            private set
            {
                _user.ChineseZodiac = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand<object> SignsRequestedCommand =>
            _signsRequested ??= new RelayCommand<object>(_ => GetSigns());

        #endregion

        private void GetSigns()
        {
           throw new NotImplementedException();
        }

        private void DateChanged()
        {
            Age = CalculateAge(DateOfBirth);
            if (IsBirthday(DateOfBirth))
                MessageBox.Show("Happy birthday!");
        }

        #region AgeValidationMethods

        private int CalculateAge(DateTime dob)
        {
            var age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.Month < dob.Month ||
                (DateTime.Now.Month == dob.Month && DateTime.Now.Day < dob.Day))
                age--;
            return age;
        }

        private bool IsBirthday(DateTime dob)
        {
            if (!IsValidAge(CalculateAge(dob)))
                MessageBox.Show("Invalid date of birth");
            return DateTime.Now.DayOfYear == dob.DayOfYear;
        }

        private bool IsValidAge(int age)
        {
            return age is >= 0 and < 135;
        }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}