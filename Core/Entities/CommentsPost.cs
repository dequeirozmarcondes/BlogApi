namespace BlogApi.Core.Entities
{
    public class CommentsPost
    {
        public string Id { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
        public Post Post { get; set; } = null!; // Using null-forgiving operator
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!; // Using null-forgiving operator
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public CommentsPost(string id, string postId, string userId, ApplicationUser user, string content, DateTime createdAt)
        {
            Id = id;
            PostId = postId;
            UserId = userId;
            User = user;
            Content = content;
            CreatedAt = createdAt;
        }

        public CommentsPost() { }
    }
}