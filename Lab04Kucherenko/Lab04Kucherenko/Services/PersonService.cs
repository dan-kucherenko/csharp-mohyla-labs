using KMA.Lab04.Kucherenko.Models;
using KMA.Lab04.Kucherenko.Repositories;
using System.Collections.Generic;

namespace KMA.Lab04.Kucherenko.Services
{
    internal class PersonService
    {
        private static FileRepository Repository = new FileRepository();

        public List<SavedPerson> GetAllPersons()
        {
            var res = new List<SavedPerson>();
            foreach (var person in Repository.GetAll())
                res.Add(new SavedPerson(person.FirstName, person.LastName, person.Email, person.DateOfBirth,
                                            person.IsAdult, person.SunSign, person.ChineseSign, person.IsBirthday));
            return res;
        }
    }
}