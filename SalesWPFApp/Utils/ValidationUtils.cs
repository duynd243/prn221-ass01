using System.Text.RegularExpressions;

namespace SalesWPFApp.Utils;

public static class ValidationUtils
{
    public static bool IsValidEmail(string email)
    {
        var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(email);
    }
}