using System.ComponentModel.DataAnnotations;

namespace AmsHelpdeskApi.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress] public string Email { get; set; }

        [Required]
        [MinLength(6)] public string Password { get; set; }
    }
}
