namespace Bitter.Blazor.Helpers.Extensions;

public static class DependencyInjection
{
    public static void AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<PasswordValidator>();
        services.AddScoped<UsernameValidator>();
    }
}