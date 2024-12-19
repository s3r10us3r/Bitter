using Bitter.Api.Services.Interfaces;

namespace Bitter.Api.Services;

public class FakeEmailService : IEmailService
{
    private readonly string _frontUrl;
    
    public FakeEmailService(string frontUrl)
    {
        _frontUrl = frontUrl;
    }

    public async Task SendRegisterVerificationEmail(string email, string registerId, string login)
    {
        var responseText = $"""
                            SENT TO {email}
                            
                            Hi {login}, to register to bitter click the link below!
                            {_frontUrl}/verifyRegister/?id={registerId}
                            """;
        Console.WriteLine(responseText);
    }

    public async Task SendLoginVerificationEmail(string email, string verificationString, string login)
    {
        var responseText = $"""
                            SENT TO {email}
                            
                            Hi {login}, below is your bitter verification code!
                            {verificationString}
                            """;
    }
}