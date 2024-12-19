using Bitter.Dal.Interfaces;
using Bitter.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Bitter.Dal;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(BitterDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetOneFromMailAsync(string email)
    {
        var user = await Table.FirstOrDefaultAsync(u => u.Mail == email);
        return user;
    }

    public async Task<User?> GetOneFromLoginAsync(string login)
    {
        var user = await Table.FirstOrDefaultAsync(u => u.Username == login);
        return user;
    }

    public async Task<User?> GetOneFromRefreshTokenAsync(string refreshToken)
    {
        var user = await Table.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        return user;
    }
}