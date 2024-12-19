using System.ComponentModel.DataAnnotations;

namespace Bitter.Shared.Dtos;

public class VerifyLoginDto
{
    [Required]
    public string RequestId { get; set; }
    [Required]
    public string Code { get; set; }
}