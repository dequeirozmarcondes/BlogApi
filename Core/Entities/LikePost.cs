namespace BlogApi.Core.Entities
{
    public class LikePost
    {
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!; // Usando o operador null-forgiving

        public string PostId { get; set; } = string.Empty;
        public Post Post { get; set; } = null!; // Usando o operador null-forgiving

        public LikePost(string userId, string postId, ApplicationUser user, Post post)
        {
            UserId = userId;
            User = user;
            PostId = postId;
            Post = post;
        }

        public LikePost() { }
    }
}