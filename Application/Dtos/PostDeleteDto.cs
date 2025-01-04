using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class PostDeleteDto
    {
        [Required]
        public required string Id { get; set; }
    }
}
