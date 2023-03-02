using KMA.Lab01Kucherenko.Models;
using KMA.Lab01Kucherenko.Tools;
using System;

namespace KMA.Lab01Kucherenko.ViewModels
{
    internal class ZodiacViewModel
    {
        private User _user = new User();

        #region Properties

        public int Age
        {
            get => _user.Age;
            set => _user.Age = value;
        }

        public DateTime DateOfBirth
        {
            get => _user.DateOfBirth;
            set => _user.DateOfBirth = value;
        }

        public ZodiacSigns Zodiac
        {
            get => _user.Zodiac;
            set => _user.Zodiac = value;
        }

        public ChineseZodiacSigns ChineseZodiac
        {
            get => _user.ChineseZodiac;
            set => _user.ChineseZodiac = value;
        }

        #endregion

        public void DateChanged()
        {
            CalculateAge(DateOfBirth);
        }

        private void CalculateAge(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}