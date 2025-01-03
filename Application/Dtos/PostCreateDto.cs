﻿using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class PostCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }
    }
}
