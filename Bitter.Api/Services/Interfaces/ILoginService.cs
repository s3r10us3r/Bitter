using Bitter.Shared.Models;

namespace Bitter.Api.Services.Interfaces;

public interface ILoginService
{
    Task<string> CreateLoginRequest(User user);
    bool VerifyLoginRequest(string requestId, string code, out int userId);
}