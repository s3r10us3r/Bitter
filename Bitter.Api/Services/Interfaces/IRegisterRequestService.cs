using Bitter.Shared.Dtos;
using Bitter.Shared.Models;

namespace Bitter.Api.Services.Interfaces;

public interface IRegisterRequestService
{
    public Task<string> CreateRequest(RegisterDto dto);

    public bool TryGetUserFromRequest(string registerRequestId, out User? user);
}