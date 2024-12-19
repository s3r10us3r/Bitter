using Bitter.Shared.Models;

namespace Bitter.Dal.Interfaces;

public interface IUserRepo : IRepo<User>
{
    Task<User?> GetOneFromMailAsync(string email);
    Task<User?> GetOneFromLoginAsync(string login);
    Task<User?> GetOneFromRefreshTokenAsync(string refreshToken);
}