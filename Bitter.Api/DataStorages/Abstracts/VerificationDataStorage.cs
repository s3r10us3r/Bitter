using Bitter.Api.VerificationRequests;
using Microsoft.Extensions.Caching.Memory;

namespace Bitter.Api.DataStorages.Abstracts;

public abstract class VerificationDataStorage<T> where T : VerificationRequest
{
    private readonly MemoryCache _pendingVerificationsMemoryCache = new MemoryCache(new MemoryCacheOptions());
    private readonly TimeSpan _timeout;

    protected VerificationDataStorage(int timeoutInMinutes)
    {
        _timeout = new TimeSpan(timeoutInMinutes);
    }
    
    public void AddVerificationRequest(T request)
    {
        _pendingVerificationsMemoryCache.CreateEntry(request.Id);
        _pendingVerificationsMemoryCache.Set(request.Id, request, _timeout);
    }

    public T? TryGetVerificationRequest(string id)
    {
        if (_pendingVerificationsMemoryCache.TryGetValue(id, out var result))
        {
            _pendingVerificationsMemoryCache.Remove(id);
            return result as T;
        }
        return null;
    }
}