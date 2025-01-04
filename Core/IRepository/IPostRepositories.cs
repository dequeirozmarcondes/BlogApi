using BlogApi.Core.Entities;

namespace BlogApi.Core.IRepository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(string id);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(string id);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId);
    }
}