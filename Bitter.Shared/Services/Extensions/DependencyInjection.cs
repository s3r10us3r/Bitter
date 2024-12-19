using Bitter.Shared.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bitter.Shared.Services.Extensions;

public static class DependencyInjection
{
    public static void AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterRequestValidator, RegisterRequestValidator>();
    }
}