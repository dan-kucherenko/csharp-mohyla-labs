using KMA.Lab02.Kucherenko.Tools.Exceptions;
using KMA.Lab02.Kucherenko.Tools.Signs;
using KMA.Lab02.Kucherenko.Tools.Validators;
using System;
using System.Windows;

namespace KMA.Lab02.Kucherenko.Models
{
    internal class Person
    {
        #region Fields

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dob = DateTime.Now;
        private readonly bool _isAdult;
        private readonly SunSign _sunSign;
        private readonly ChineseSign _chineseSign;
        private readonly bool _isBirthday;

        #endregion

        #region Constructors

        public Person(String firstName, String lastName, String email, DateTime dob)
        {
            try
            {
                ValidateInput(firstName, lastName, email, dob);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dob = dob;
            _isAdult = CalculateAge(_dob) >= 18;
            _sunSign = GetZodiacSign();
            _chineseSign = GetChineseZodiacSigns();
            _isBirthday = CalculateIsBirthday();
        }

        public Person(String firstName, String lastName, String email) : this(firstName, lastName, email,
            DateTime.Now)
        {
        }

        public Person(String firstName, String lastName, DateTime dob) : this(firstName, lastName, "", dob)
        {
        }

        #endregion

        #region Properties

        public string FirstName
        {
            get { return _firstName; }
            private set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            private set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            private set { _email = value; }
        }

        public DateTime DateOfBirth
        {
            get { return _dob; }
            private set { _dob = value; }
        }

        public bool IsAdult => _isAdult;
        public SunSign SunSign => _sunSign;
        public ChineseSign ChineseSign => _chineseSign;
        public bool IsBirthday => _isBirthday;

        #endregion

        #region CalculateAge/Birthday

        private int CalculateAge(DateTime dob)
        {
            var age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.Month < dob.Month ||
                (DateTime.Now.Month == dob.Month && DateTime.Now.Day < dob.Day))
                age--;
            if (dob.CompareTo(DateTime.Now) > 0)
                return -1;
            return age;
        }

        private bool CalculateIsBirthday()
        {
            bool isBirthday = DateTime.Now.Day == DateOfBirth.Day && DateTime.Now.Month == DateOfBirth.Month;
            if (isBirthday)
                MessageBox.Show("Happy birthday!");
            return isBirthday;
        }

        private bool AgeIsMoreThanZero(int age)
        {
            return age >= 0;
        }

        private bool AgeIsLessThanPossible(int age)
        {
            return age < 135;
        }

        #endregion

        #region CalculateZodiacSign

        private SunSign GetZodiacSign()
        {
            int month = DateOfBirth.Month;
            int day = DateOfBirth.Day;

            switch (month)
            {
                case 1:
                    return (day <= 19) ? SunSign.Capricorn : SunSign.Aquarius;
                case 2:
                    return (day <= 18) ? SunSign.Aquarius : SunSign.Pisces;
                case 3:
                    return (day <= 20) ? SunSign.Pisces : SunSign.Aries;
                case 4:
                    return (day <= 19) ? SunSign.Aries : SunSign.Taurus;
                case 5:
                    return (day <= 20) ? SunSign.Taurus : SunSign.Gemini;
                case 6:
                    return (day <= 20) ? SunSign.Gemini : SunSign.Cancer;
                case 7:
                    return (day <= 22) ? SunSign.Cancer : SunSign.Leo;
                case 8:
                    return (day <= 22) ? SunSign.Leo : SunSign.Virgo;
                case 9:
                    return (day <= 22) ? SunSign.Virgo : SunSign.Libra;
                case 10:
                    return (day <= 22) ? SunSign.Libra : SunSign.Scorpio;
                case 11:
                    return (day <= 21) ? SunSign.Scorpio : SunSign.Sagittarius;
                default:
                    return (day <= 21) ? SunSign.Sagittarius : SunSign.Capricorn;
            }
        }

        private ChineseSign GetChineseZodiacSigns()
        {
            return (ChineseSign)(DateOfBirth.Year % 12);
        }

        #endregion

        public bool IsAnyFieldNull()
        {
            if (!String.IsNullOrWhiteSpace(FirstName) ||
                !String.IsNullOrWhiteSpace(LastName) ||
                !String.IsNullOrWhiteSpace(Email)
               )
                return false;
            return true;
        }

        private void ValidateInput(string firstName, string lastName, string email, DateTime dob)
        {
            if (String.IsNullOrWhiteSpace(firstName) ||
                String.IsNullOrWhiteSpace(lastName) ||
                !NameValidator.IsValidName(firstName) ||
                !NameValidator.IsValidName(lastName))
                throw new InvalidPersonNameException();

            if (String.IsNullOrWhiteSpace(email) || !EmailValidator.IsValidEmail(email))
                throw new InvalidEmailException();

            var age = CalculateAge(dob);
            if (!AgeIsMoreThanZero(age))
                throw new FutureDobException();
            if (!AgeIsLessThanPossible(age))
                throw new TooOldDobException();
        }
    }
}