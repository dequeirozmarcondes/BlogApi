using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class UpdatePostDto
    {
        [Required]
        public required string Id { get; set; }
        [Required]
        public required string PostId { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string Content { get; set; }
    }
}