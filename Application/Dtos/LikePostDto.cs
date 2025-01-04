using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class LikePostDto
    {
        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string PostId { get; set; }
    }
}
