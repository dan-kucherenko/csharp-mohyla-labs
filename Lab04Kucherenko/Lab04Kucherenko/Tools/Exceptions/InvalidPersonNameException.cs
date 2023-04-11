using System;

namespace KMA.Lab04.Kucherenko.Tools.Exceptions
{
    internal class InvalidPersonNameException : Exception
    {
        public InvalidPersonNameException() : base("Invalid name or surname provided. Please, try again.")
        {
        }
    }
}