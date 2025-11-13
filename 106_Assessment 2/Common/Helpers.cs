using System.Text.RegularExpressions;

namespace _106_Assessment_2.Common
{
    public static class Helpers
    {
        public static bool IsPasswordValid(string password)
        {
            // Check minimum length
            if (password.Length < 6)
                return false;

            // Check for uppercase letter
            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            // Check for lowercase letter
            if (!Regex.IsMatch(password, @"[a-z]"))
                return false;

            // Check for digit
            if (!Regex.IsMatch(password, @"[0-9]"))
                return false;

            // If all conditions pass
            return true;
        }

    }
}
