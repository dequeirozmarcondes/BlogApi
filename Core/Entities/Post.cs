using System.Collections.Generic;

namespace BlogApi.Core.Entities
{
    public class Post
    {
        public string Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;

        public ICollection<CommentsPost> CommentsPosts { get; set; } = new List<CommentsPost>();
        public ICollection<LikePost> LikePosts { get; set; } = new List<LikePost>();

        public Post()
        {
        }

        public Post(string id, string title, string content, string userId, ApplicationUser user)
        {
            Id = id;
            Title = title;
            Content = content;
            UserId = userId;
            User = user;
            CommentsPosts = new List<CommentsPost>(); // Inicializa a lista
            LikePosts = new List<LikePost>(); // Inicializa a lista
        }

        public Post(string id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
            CommentsPosts = new List<CommentsPost>(); // Inicializa a lista
            LikePosts = new List<LikePost>(); // Inicializa a lista
        }
    }
}