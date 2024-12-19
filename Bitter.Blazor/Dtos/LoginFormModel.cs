namespace Bitter.Blazor.Dtos;

public class LoginFormModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; }
}