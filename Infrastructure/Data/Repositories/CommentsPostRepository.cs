using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using Raven.Client.Documents.Session;
using Raven.Client.Documents;

namespace BlogApi.Infrastructure.Data.Repositories
{
    public class CommentsPostRepository(IAsyncDocumentSession session) : ICommentsPostRepository
    {
        public async Task<IEnumerable<CommentsPost>> GetAllCommentsPostsAsync()
        {
            return await session.Query<CommentsPost>().ToListAsync();
        }

        public async Task<CommentsPost> GetCommentsPostByIdAsync(string commentsPostId)
        {
            return await session.LoadAsync<CommentsPost>(commentsPostId);
        }

        public async Task AddCommentsPostAsync(CommentsPost commentsPost)
        {
            await session.StoreAsync(commentsPost);
            await session.SaveChangesAsync();
        }

        public async Task UpdateCommentsPostAsync(CommentsPost commentsPost)
        {
            var existingCommentsPost = await session.LoadAsync<CommentsPost>(commentsPost.Id);

            if (existingCommentsPost != null)
            {
                existingCommentsPost.Content = commentsPost.Content;
                existingCommentsPost.UserId = commentsPost.UserId;
                existingCommentsPost.CreatedAt = commentsPost.CreatedAt;
                await session.SaveChangesAsync();
            }
        }

        public async Task DeleteCommentsPostAsync(string commentsPostId)
        {
            var commentsPost = await GetCommentsPostByIdAsync(commentsPostId);
            if (commentsPost != null)
            {
                session.Delete(commentsPost);
                await session.SaveChangesAsync();
            }
        }
    }
}