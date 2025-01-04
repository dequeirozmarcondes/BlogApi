using BlogApi.Core.Entities;

namespace BlogApi.Application.IServices
{
    public interface ICommentsPostService
    {
        Task<IEnumerable<CommentsPost>> GetAllCommentsPostsAsync();
        Task<CommentsPost> GetCommentsPostByIdAsync(string commentsPostId);
        Task AddCommentsPostAsync(CommentsPost commentsPost);
        Task UpdateCommentsPostAsync(CommentsPost commentsPost);
        Task DeleteCommentsPostAsync(string commentsPostId);
    }
}