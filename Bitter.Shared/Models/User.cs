using System.ComponentModel.DataAnnotations;

namespace Bitter.Shared.Models;

public class User
{
    [Required] public int Id { get; set; }
    
    [Required]
    [StringLength(64, MinimumLength = 6)]
    public string Username { get; set; } = "";

    [Required]
    [StringLength(300, MinimumLength = 6)] //Maximum mail length is something under 300
    public string Mail { get; set; } = "";
    
    [Required] public string PasswordHash { get; set; } = "";

    public string? RefreshToken { get; set; } = "";
}