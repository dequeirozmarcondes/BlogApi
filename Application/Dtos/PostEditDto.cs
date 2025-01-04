using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class PostEditDto
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }
    }
}
