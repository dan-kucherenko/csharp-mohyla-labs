using System;

namespace KMA.Lab02.Kucherenko.Models
{
    internal class PersonService
    {
        private static Person _person;

        public Person Person
        {
            get => _person;
            private set => _person = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void SavePerson(Person person)
        {
            Person = person;
        }
    }
}
