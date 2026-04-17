using System.ComponentModel.DataAnnotations;

namespace AmsHelpdeskApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "User";
    }
}
