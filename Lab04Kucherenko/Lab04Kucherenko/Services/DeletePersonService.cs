using KMA.Lab04.Kucherenko.Models;
using KMA.Lab04.Kucherenko.Repositories;
using System.Windows;

namespace KMA.Lab04.Kucherenko.Services
{
    internal class DeletePersonService
    {
        private static FileRepository Repository = new FileRepository();

        public void DeletePerson(SavedPerson person)
        {
            Repository.Delete(person);
            MessageBox.Show($"Person with email {person.Email} was deleted");
        }
    }
}
