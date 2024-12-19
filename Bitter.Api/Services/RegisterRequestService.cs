using Bitter.Api.DataStorages;
using Bitter.Api.Services.Interfaces;
using Bitter.Api.VerificationRequests;
using Bitter.Shared.Dtos;
using Bitter.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace Bitter.Api.Services;

public class RegisterRequestService : IRegisterRequestService
{
    private readonly IPasswordHasher<object> _passwordHasher;
    private readonly RegisterVerificationDataStorage _emailVerificationDataStorage;
    private readonly IEmailService _emailService;
    public RegisterRequestService(IPasswordHasher<object> passwordHasher, IEmailService emailService,
        RegisterVerificationDataStorage emailVerificationDataStorage)
    {
        _passwordHasher = passwordHasher;
        _emailVerificationDataStorage = emailVerificationDataStorage;
        _emailService = emailService;
    }
    
    public async Task<string> CreateRequest(RegisterDto dto)
    {
        //default password hasher does not need object passing
        var passwordHash = _passwordHasher.HashPassword(null!, dto.Password);
        var guid = Guid.NewGuid().ToString();
        var registerRequest = new RegisterVerificationRequest
        {
            Id = guid,
            Email = dto.Email,
            Username = dto.Login,
            PasswordHash = passwordHash
        };
        _emailVerificationDataStorage.AddVerificationRequest(registerRequest);
        await _emailService.SendRegisterVerificationEmail(dto.Email, registerRequest.Id, registerRequest.Username);
        return registerRequest.Id;
    }

    public bool TryGetUserFromRequest(string registerRequestId, out User? user)
    {
        user = null;
        var registerRequest = _emailVerificationDataStorage.TryGetVerificationRequest(registerRequestId);
        if (registerRequest is null)
            return false;
        
        user = new User
        {
            Username = registerRequest.Username,
            Mail = registerRequest.Email,
            PasswordHash = registerRequest.PasswordHash
        };
        return true;
    }
}