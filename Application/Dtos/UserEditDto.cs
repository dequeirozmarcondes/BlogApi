using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class UserEditDto
    {
        [Required]
        public required string Id { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public required string Bio { get; set; }
    }
}
