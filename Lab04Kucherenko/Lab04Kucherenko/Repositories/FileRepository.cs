using KMA.Lab04.Kucherenko.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KMA.Lab04.Kucherenko.Tools;
using Path = System.IO.Path;

namespace KMA.Lab04.Kucherenko.Repositories
{
    internal class FileRepository
    {
        private static readonly string BaseFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PersonStorage");

        public async void AddInitialPersons()
        {
            if (!Directory.EnumerateFileSystemEntries(BaseFolder).Any())
                foreach (var person in InitialList.InitialPeople)
                    await AddOrUpdateAsync(person);
        }

        public FileRepository()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public async Task AddOrUpdateAsync(Person obj)
        {
            var stringObj = JsonSerializer.Serialize(obj);
            await using StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, obj.Email) + ".txt", false);
            await sw.WriteAsync(stringObj);
        }

        public void Delete(SavedPerson obj)
        {
            string deletingObjFilePath = Path.Combine(BaseFolder, obj.Email) + ".txt";
            if (!File.Exists(deletingObjFilePath))
                return;
            File.Delete(deletingObjFilePath);
        }

        public async Task<SavedPerson> GetAsync(string email)
        {
            string filePath = Path.Combine(BaseFolder, email);
            if (!File.Exists(filePath))
                return null;
            string stringObj = null;
            using (StreamReader sr = new StreamReader(filePath))
            {
                stringObj = await sr.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<SavedPerson>(stringObj);
        }

        public List<SavedPerson> GetAll()
        {
            var res = new List<SavedPerson>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;
                using (StreamReader sr = new StreamReader(file))
                {
                    stringObj = sr.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<SavedPerson>(stringObj));
            }

            return res;
        }
    }
}