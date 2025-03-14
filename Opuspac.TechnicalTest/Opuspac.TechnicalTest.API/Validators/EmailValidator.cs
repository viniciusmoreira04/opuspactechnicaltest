using System.Text.RegularExpressions;

namespace Opuspac.TechnicalTest.Portal.Server.Validators;

public static class EmailValidator
{
    private static readonly Regex EmailPattern = EmailValidatorRegex();

    public static bool IsValid(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && EmailPattern.IsMatch(email);
    }

    private static Regex EmailValidatorRegex() => new("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
}