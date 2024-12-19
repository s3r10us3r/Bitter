namespace Bitter.Api.VerificationRequests;

public class LoginVerificationRequest : VerificationRequest
{
    public int UserId { get; set; }
    public string VerificationCode { get; set; } = "";
}