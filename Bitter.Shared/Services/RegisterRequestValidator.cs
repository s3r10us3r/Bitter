using System.Net.Mail;
using Bitter.Shared.Dtos;
using Bitter.Shared.Services.Interfaces;

namespace Bitter.Shared.Services;

public class RegisterRequestValidator : IRegisterRequestValidator
{
    public bool IsRegisterDtoValid(RegisterDto registerDto, out string message)
    {
        if (!IsMailValid(registerDto.Email))
        {
            message = "Invalid mail.";
            return false;
        }

        if (!IsLoginValid(registerDto.Login, out message))
        {
            return false;
        }

        if (!IsPasswordValid(registerDto.Password, out message))
        {
            return false;
        }

        message = "";
        return true;
    }

    private bool IsMailValid(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }

    private bool IsLoginValid(string login, out string message)
    {
        message = "";
        if (login.Length < 6)
        {
            message = "Login is too short.";
            return false;
        }

        if (login.Length > 64)
        {
            message = "Login is too long.";
            return false;
        }

        const string legalSpecialCharacters = "_-";
        
        if (login.Any(c => !char.IsAsciiLetter(c) && !char.IsDigit(c) && !legalSpecialCharacters.Contains(c)))
        {
            message = "Login must consist only of digits, ascii letters and '-', '_'.";
            return false;
        }
        return true;
    }

    private bool IsPasswordValid(string password, out string message)
    {
        message = "";
        if (password.Length < 8)
        {
            message = "Password is too short.";
            return false;
        }

        if (password.Length > 256)
        {
            message = "Password is too long.";
            return false;
        }

        if (password.Any(c => !char.IsAscii(c)))
        {
            message = "Password contains an illegal character.";
            return false;
        }

        if (!(password.Any(char.IsAsciiLetterLower) && password.Any(char.IsAsciiLetterUpper) &&
              password.Any(char.IsAsciiDigit) && password.Any(char.IsPunctuation)))
        {
            message = "Password must contain a large and small letter a digit and a special character";
            return false;
        }

        return true;
    }
}