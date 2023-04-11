using KMA.Lab04.Kucherenko.Models;
using KMA.Lab04.Kucherenko.Repositories;
using System;
using System.Threading.Tasks;

namespace KMA.Lab04.Kucherenko.Services
{
    internal class AddPersonService
    {
        private static FileRepository Repository = new FileRepository();
        public bool IsAnyFieldNull(Person person)
        {
            if (!String.IsNullOrWhiteSpace(person.FirstName) ||
                !String.IsNullOrWhiteSpace(person.LastName) ||
                !String.IsNullOrWhiteSpace(person.Email)
               )
                return false;
            return true;
        }
        public async Task<bool> AddPerson(Person person)
        {
            var regPerson = await Repository.GetAsync(person.Email);
            if (regPerson != null)
                throw new Exception("User already exists");
            await Repository.AddOrUpdateAsync(person);
            return true;
        }
    }
}
