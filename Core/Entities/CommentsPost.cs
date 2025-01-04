using System;

namespace BlogApi.Core.Entities
{
    public class CommentsPost
    {
        public required string Id { get; set; }
        public required string PostId { get; set; }
        public required Post Post { get; set; }
        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public CommentsPost(string id, string postId, Post post, string userId, ApplicationUser user, string content, DateTime createdAt)
        {
            Id = id;
            PostId = postId;
            Post = post;
            UserId = userId;
            User = user;
            Content = content;
            CreatedAt = createdAt;
        }

        public CommentsPost()
        {
            CreatedAt = DateTime.UtcNow; // Inicializando a data de criação com o valor atual
        }
    }
}