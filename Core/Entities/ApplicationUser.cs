using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogApi.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<LikePost> LikePosts { get; set; }
        public ICollection<CommentsPost> CommentsPosts { get; set; }

        public ApplicationUser()
        {
            Posts = [];
            LikePosts = [];
            CommentsPosts = [];
        }
    }
}