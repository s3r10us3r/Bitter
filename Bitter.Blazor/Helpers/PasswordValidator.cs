using Bitter.Blazor.Extensions;

namespace Bitter.Blazor.Helpers;

public class PasswordValidator
{
    public bool IsPasswordValid(string password, out string errorMessage)
    {
        if (password.Length < 8)
        {
            errorMessage = "Password must be at least 8 characters long.";
            return false;
        }

        if (password.Length > 64)
        {
            errorMessage = "Password cannot be longer than 64 characters.";
            return false;
        }

        if (!HasCapitalLetter(password))
        {
            errorMessage = "Password has to include a capital letter.";
            return false;
        }

        if (!HasSmallLetter(password))
        {
            errorMessage = "Password hsa to include a small letter.";
            return false;
        }

        if (!HasDigit(password))
        {
            errorMessage = "Password has to include a digit.";
            return false;
        }

        if (!HasSpecialCharacter(password))
        {
            errorMessage = "Password must contain a special character.";
            return false;
        }

        if (HasIllegalCharacter(password))
        {
            errorMessage = "Password has an illegal character.";
            return false;
        }

        errorMessage = "";
        return true;
    }

    private bool HasCapitalLetter(string password)
        => password.Any(char.IsUpper);

    private bool HasSmallLetter(string password)
        => password.Any(char.IsLower);

    private bool HasDigit(string password)
        => password.Any(char.IsDigit);

    private bool HasSpecialCharacter(string password)
        => password.Any(c => !char.IsLetter(c));
    
    private bool HasIllegalCharacter(string password)
        => password.Any(c => char.IsWhiteSpace(c) || char.IsControl(c));
}