namespace Bitter.Api.DataStorages.Extensions;

public static class DependencyInjection
{
    public static void AddDataStorages(this IServiceCollection services)
    {
        services.AddSingleton(new LoginAttemptsDataStorage(5, 15));
        services.AddSingleton(new LoginVerificationDataStorage(5));
        services.AddSingleton(new RegisterVerificationDataStorage(15));
    }
}