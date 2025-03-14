using Opuspac.TechnicalTest.Application.DTOs;

namespace Opuspac.TechnicalTest.Portal.Server.Validators;

public static class UserValidator
{
    public static bool IsUserValid(CreateUserDTO user, out string fielsWithError)
    {
        user ??= new CreateUserDTO();
        List<string> errors = new();

        if (string.IsNullOrWhiteSpace(user.Name))
        {
            errors.Add("Nome");
        }
        if (string.IsNullOrWhiteSpace(user.Password))
        {
            errors.Add("Senha");
        }
        if (!string.IsNullOrWhiteSpace(user.Email) && !EmailValidator.IsValid(user.Email))
        {
            errors.Add("Email");
        }

        fielsWithError = errors.Count > 0 ? string.Join(". ", errors) : null;
        return errors.Count == 0;
    }
}