using Bitter.Dal.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bitter.Dal.Extensions;

public static class DependencyInjection
{
    public static void AddRepos(this IServiceCollection services)
    {
        services.AddScoped<IUserRepo, UserRepo>();
    }
}