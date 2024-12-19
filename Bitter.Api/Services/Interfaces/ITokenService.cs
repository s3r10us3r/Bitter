using Bitter.Shared.Models;

namespace Bitter.Api.Services.Interfaces;

public interface ITokenService
{
    string GenerateLoginJwt(User user);
    string GenerateRefreshToken();
}