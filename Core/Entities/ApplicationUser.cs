using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogApi.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string Bio { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<LikePost> LikePosts { get; set; }
        public ICollection<CommentsPost> CommentsPosts { get; set; }

        // Construtor sem parâmetros
        public ApplicationUser()
        {
            Posts = new List<Post>();
            LikePosts = new List<LikePost>();
            CommentsPosts = new List<CommentsPost>();
        }

        // Construtor com parâmetros para inicializar todas as propriedades obrigatórias
        public ApplicationUser(string bio)
        {
            Bio = bio;
            Posts = new List<Post>();
            LikePosts = new List<LikePost>();
            CommentsPosts = new List<CommentsPost>();
        }
    }
}