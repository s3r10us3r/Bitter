using System.Runtime.InteropServices;
using Bitter.Api.DataStorages;
using Bitter.Api.Services.Interfaces;
using Bitter.Api.VerificationRequests;
using Bitter.Shared.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace Bitter.Api.Services;

public class LoginService : ILoginService
{
    private readonly LoginVerificationDataStorage _loginVerificationDataStorage;
    private readonly IEmailService _emailService;
    private const int VerificationStringLength = 6;
    
    public LoginService(LoginVerificationDataStorage loginVerificationDataStorage, IEmailService emailService)
    {
        _loginVerificationDataStorage = loginVerificationDataStorage;
        _emailService = emailService;
    }
    
    public async Task<string> CreateLoginRequest(User user)
    {
        var request = new LoginVerificationRequest
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            VerificationCode = GenerateVerificationCode()
        };
        await _emailService.SendLoginVerificationEmail(user.Mail, request.VerificationCode, user.Username);
        _loginVerificationDataStorage.AddVerificationRequest(request);
        return request.Id;
    }

    public bool VerifyLoginRequest(string requestId, string code, out int userId)
    {
        userId = 0;
        var loginRequest = _loginVerificationDataStorage.TryGetVerificationRequest(requestId);
        if (loginRequest != null)
        {
            userId = loginRequest.UserId;
            return code == loginRequest.VerificationCode;
        }

        return false;
    }

    private string GenerateVerificationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();

        return new string(Enumerable.Repeat(chars, VerificationStringLength)
            .Select(s => s[random.Next(chars.Length)]).ToArray());
    }
}