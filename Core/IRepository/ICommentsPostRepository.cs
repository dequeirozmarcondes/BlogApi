using BlogApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Core.IRepository
{
    public interface ICommentsPostRepository
    {
        Task<IEnumerable<CommentsPost>> GetAllCommentsPostsAsync();
        Task<CommentsPost> GetCommentsPostByIdAsync(string commentsPostId);
        Task AddCommentsPostAsync(CommentsPost commentsPost);
        Task UpdateCommentsPostAsync(CommentsPost commentsPost);
        Task DeleteCommentsPostAsync(string commentsPostId);
    }
}