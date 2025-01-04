using BlogApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Core.IRepository
{
    public interface ILikePostRepository
    {
        Task<IEnumerable<LikePost>> GetAllLikePostsAsync();
        Task<LikePost> GetLikePostByIdAsync(string userId, string postId);
        Task<IEnumerable<LikePost>> GetLikePostsByPostIdAsync(string postId);
        Task AddLikePostAsync(LikePost likePost);
        Task UpdateLikePostAsync(LikePost likePost);
        Task DeleteLikePostAsync(string userId, string postId);
    }
}