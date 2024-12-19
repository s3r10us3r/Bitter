using System.Runtime.InteropServices;

namespace Bitter.Blazor.Helpers;

public class UsernameValidator
{
    public bool IsUsernameValid(string username, out string errorMessage)
    {
        if (username.Length < 6)
        {
            errorMessage = "Username must be at least 6 characters long.";
            return false;
        }

        if (username.Length > 64)
        {
            errorMessage = "Username can't be longer than 64 characters.";
            return false;
        }

        if (DoesContainIllegalCharacter(username))
        {
            errorMessage = "Username can consist of large and small letters, digits or '_' and '-'.";
            return false;
        }

        errorMessage = "";
        return true;
    }

    private bool DoesContainIllegalCharacter(string username)
        => username.Any(c => !char.IsLetter(c) && !char.IsDigit(c) && c != '-' && c != '_');
}