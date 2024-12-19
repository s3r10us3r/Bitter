using Bitter.Api.DataStorages.Abstracts;
using Bitter.Api.VerificationRequests;

namespace Bitter.Api.DataStorages;

public class LoginVerificationDataStorage : VerificationDataStorage<LoginVerificationRequest>
{
    public LoginVerificationDataStorage(int timeoutInMinutes) : base(timeoutInMinutes)
    {
    }
}