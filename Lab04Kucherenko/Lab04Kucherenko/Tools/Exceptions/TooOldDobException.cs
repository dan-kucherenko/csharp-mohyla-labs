using System;

namespace KMA.Lab04.Kucherenko.Tools.Exceptions
{
    internal class TooOldDobException : Exception
    {
        public TooOldDobException() : base("You can't be that old enough, according to the given date. Please, try again.")
        {
        }
    }
}