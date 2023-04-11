using System;

namespace KMA.Lab02.Kucherenko.Tools.Exceptions
{
    internal class FutureDobException : Exception
    {
        public FutureDobException() : base("The person can't be younger than 0 y.o., according to the given date. Please try again.")
        {
        }
    }
}
