using System;

namespace KMA.Lab04.Kucherenko.Tools.Exceptions
{
    internal class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("You provided invalid email address. Please try again.")
        {
        }
    }
}