using System.ComponentModel.DataAnnotations;

namespace Bitter.Shared.Dtos;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(64, MinimumLength = 6)]
    public string Login { get; set; } = "";

    [Required]
    [StringLength(256, MinimumLength = 8)]
    public string Password { get; set; } = "";
}