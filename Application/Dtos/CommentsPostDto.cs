using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class CommentsPostDto
    {
        public string? Id { get; set; }

        [Required]
        public required string PostId { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}