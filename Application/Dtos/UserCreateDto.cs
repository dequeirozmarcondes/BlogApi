using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public required string Bio { get; set; }
    }
}
