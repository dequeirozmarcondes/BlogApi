using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class PostListDto
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        public required string UserId { get; set; }

        public List<LikePostDto> Likes { get; set; } = new();
        public List<CommentsGetAllDto> Comments { get; set; } = new();
    }
}
