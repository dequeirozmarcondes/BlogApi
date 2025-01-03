namespace BlogApi.Core.Entities
{
    public class Post
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = default!;

        public ICollection<CommentsPost> CommentsPosts { get; set; }
        public ICollection<LikePost> LikePosts { get; set; }
        public string V { get; }

        public Post(string id, string title, string content, string userId, ApplicationUser user)
        {
            Id = id;
            Title = title;
            Content = content;
            UserId = userId;
            User = user;
            CommentsPosts = [];
            LikePosts = [];
        }

        public Post()
        {
            CommentsPosts = [];
            LikePosts = [];
        }

        public Post(string v, string title, string content)
        {
            V = v;
            Title = title;
            Content = content;
        }
    }
}