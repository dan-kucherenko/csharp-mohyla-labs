using KMA.Lab04.Kucherenko.Tools.Signs;
using System;

namespace KMA.Lab04.Kucherenko.Models
{
    internal class SavedPerson
    {
        public SavedPerson(string firstName, string lastName, string email, DateTime dateOfBirth, 
                            bool isAdult, SunSign sunSign, ChineseSign chineseSign, bool isBirthday)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth.Date;
            IsAdult = isAdult;
            SunSign = sunSign;
            ChineseSign = chineseSign;
            IsBirthday = isBirthday;
        }

        #region Properties

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }
        public bool IsAdult { get; }
        public SunSign SunSign { get; }
        public ChineseSign ChineseSign { get; }
        public bool IsBirthday { get; }

        #endregion
    }
}
