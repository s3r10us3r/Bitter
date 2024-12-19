using Bitter.Api.Services.Interfaces;

namespace Bitter.Api.Services.Extensions;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, FakeEmailService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IRegisterRequestService, RegisterRequestService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}