using Bitter.Api.DataStorages.Abstracts;
using Bitter.Api.VerificationRequests;
using Bitter.Shared.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Caching.Memory;

namespace Bitter.Api.DataStorages;

public class RegisterVerificationDataStorage : VerificationDataStorage<RegisterVerificationRequest>
{
    public RegisterVerificationDataStorage(int timeoutInMinutes) : base(timeoutInMinutes)
    {
    }
    
}