using System.ComponentModel.DataAnnotations;

namespace Bitter.Shared.Dtos;

public class LoginDto
{
    [Required]
    [StringLength(64, MinimumLength = 6)]
    public string Username { get; set; } = "";

    [Required]
    [StringLength(256, MinimumLength = 8)]
    public string Password { get; set; } = "";
}