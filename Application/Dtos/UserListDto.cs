﻿using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class UserListDto
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}