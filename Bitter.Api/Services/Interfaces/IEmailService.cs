namespace Bitter.Api.Services.Interfaces;

public interface IEmailService
{
    private const string AppUrl = "";

    Task SendRegisterVerificationEmail(string email, string registerId, string login);

    Task SendLoginVerificationEmail(string email, string verificationString, string login);
}