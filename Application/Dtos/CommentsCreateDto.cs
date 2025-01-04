using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class CommentsCreateDto
    {
        [Required]
        public required string PostId { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string Content { get; set; }
    }
}