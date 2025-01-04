using BlogApi.Core.Entities;

namespace BlogApi.Core.IRepository
{
    public interface ICommentsPostRepository
    {
        Task<IEnumerable<CommentsPost>> GetAllCommentsPostsAsync();
        Task<CommentsPost?> GetCommentsPostByIdAsync(string commentsPostId);
        Task<IEnumerable<CommentsPost>> GetCommentsPostsByPostIdAsync(string postId);
        Task AddCommentsPostAsync(CommentsPost commentsPost);
        Task UpdateCommentsPostAsync(CommentsPost commentsPost);
        Task DeleteCommentsPostAsync(string commentsPostId);
    }
}