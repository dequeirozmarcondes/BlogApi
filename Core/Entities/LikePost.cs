namespace BlogApi.Core.Entities
{
    public class LikePost
    {
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public string PostId { get; set; } = string.Empty;
        public Post Post { get; set; } = null!;

        public LikePost(string userId, string postId, Post post)
        {
            UserId = userId;
            PostId = postId;
            Post = post;
        }

        public LikePost() { }
    }
}