using Microsoft.Extensions.Caching.Memory;

namespace Bitter.Api.DataStorages;

public class LoginAttemptsDataStorage
{
    private readonly MemoryCache _attemptsCache = new MemoryCache(new MemoryCacheOptions());

    private readonly int _maxAttempts;
    private readonly int _timeout;

    public LoginAttemptsDataStorage(int maxAttempts, int timeoutInMinutes)
    {
        _maxAttempts = maxAttempts;
        _timeout = timeoutInMinutes;
    }
    
    public bool CanLogIn(string username)
    {
        var attempts = _attemptsCache.GetOrCreate(username, _ => 0);
        if (attempts >= _maxAttempts)
        {
            return false;
        }
        _attemptsCache.Set(username, attempts + 1, TimeSpan.FromMinutes(_timeout));
        return true;
    }
}
