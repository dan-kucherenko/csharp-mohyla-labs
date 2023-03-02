using System;
using KMA.Lab01Kucherenko.Tools;

namespace KMA.Lab01Kucherenko.Models
{
    internal class User
    {
        #region Fields

        private int _age;
        private DateTime _dob = DateTime.Today;
        private ZodiacSigns _zodiac;
        private ChineseZodiacSigns _chineseZodiac;

        #endregion

        #region Properties

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public DateTime DateOfBirth
        {
            get => _dob;
            set => _dob = value;
        }

        public ZodiacSigns Zodiac
        {
            get => _zodiac;
            set => _zodiac = value;
        }

        public ChineseZodiacSigns ChineseZodiac
        {
            get => _chineseZodiac;
            set => _chineseZodiac = value;
        }

        #endregion
    }
}