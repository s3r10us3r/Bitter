namespace Bitter.Blazor.Extensions;

public static class PasswordChecking
{
    public static bool HasLargeLetter(this string password)
    => password.Any(c => char.IsLetter(c) && char.IsUpper(c));

    public static bool HasSmallLetter(this string password)
        => password.Any(c => char.IsLetter(c) && !char.IsUpper(c));

    public static bool HasDigit(this string password)
        => password.Any(char.IsDigit);

    public static bool HasSpecialCharacter(this string password)
        => password.Any(c => !char.IsLetter(c) && !char.IsDigit(c));

    public static bool HasIllegalCharacter(this string password)
        => password.Any(c => !char.IsAscii(c));
}