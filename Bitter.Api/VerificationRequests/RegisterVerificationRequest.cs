using System.ComponentModel.DataAnnotations;

namespace Bitter.Api.VerificationRequests;

public class RegisterVerificationRequest : VerificationRequest
{
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
}