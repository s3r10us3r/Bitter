using Bitter.Shared.Dtos;

namespace Bitter.Shared.Services.Interfaces;

public interface IRegisterRequestValidator
{
    public bool IsRegisterDtoValid(RegisterDto registerDto, out string message);
}