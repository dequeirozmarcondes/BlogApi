using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos
{
    public class UserDetailsDto
    {
        public string? Id { get; set; }

        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public IEnumerable<PostListDto>? Posts { get; set; }
    }
}
