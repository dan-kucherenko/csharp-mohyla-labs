using System.Text.RegularExpressions;

namespace KMA.Lab04.Kucherenko.Tools.Validators
{
    internal class NameValidator
    {
        public static bool IsValidName(string name)
        {
            string pattern = @"[a-z, A-Z]";
            return Regex.IsMatch(name, pattern);
        }
    }
}