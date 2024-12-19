using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Bitter.Api.Services.Interfaces;
using Bitter.Dal.Interfaces;
using Bitter.Shared.Models;
using Microsoft.IdentityModel.Tokens;

namespace Bitter.Api.Services;

public class TokenService : ITokenService
{
    private IUserRepo _userRepo;

    public TokenService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }
    
    public string GenerateLoginJwt(User user)
    {
        var claims = GetClaims(user);
        var key = new SymmetricSecurityKey("VerySecretSecurityKeyUnbreakableSecurity"u8.ToArray());
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        return GetToken(claims, credentials);
    }

    private Claim[] GetClaims(User user)
    {
        return new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Mail),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
    }

    private string GetToken(Claim[] claims, SigningCredentials credentials)
    {
        var token = new JwtSecurityToken(
            issuer: "bitter.com",
            audience: "bitter.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}