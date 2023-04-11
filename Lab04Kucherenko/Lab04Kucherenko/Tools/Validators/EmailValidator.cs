using System.Text.RegularExpressions;

namespace KMA.Lab04.Kucherenko.Tools.Validators
{
    internal class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^\w+[\W\w]*@\w+(\.\w+)+[\W\w]*$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
