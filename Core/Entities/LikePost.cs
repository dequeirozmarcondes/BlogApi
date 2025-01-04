namespace BlogApi.Core.Entities
{
    public class LikePost
    {
        // Propriedades obrigatórias
        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; }

        public required string PostId { get; set; }
        public required Post Post { get; set; }

        // Construtor padrão sem parâmetros
        public LikePost()
        {
        }

        // Construtor completo
        public LikePost(string userId, string postId, ApplicationUser user, Post post)
        {
            UserId = userId;
            User = user;
            PostId = postId;
            Post = post;
        }
    }
}