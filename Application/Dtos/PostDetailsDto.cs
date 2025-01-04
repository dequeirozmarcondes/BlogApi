using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class PostDetailsDto
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }
        [Required]
        public required string UserId { get; set; }
        public List<LikePostDto> Likes { get; set; } = new();
        public List<CommentsPostDto> Comments { get; set; } = new();
    }
}
