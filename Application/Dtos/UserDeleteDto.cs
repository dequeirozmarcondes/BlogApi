using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class UserDeleteDto
    {
        [Required]
        public required string Id { get; set; }
    }
}
