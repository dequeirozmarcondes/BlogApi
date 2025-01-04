using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Infrastructure.Data.Repositories
{
    public class CommentsPostRepository : ICommentsPostRepository
    {
        private readonly IAsyncDocumentSession _session;

        public CommentsPostRepository(IAsyncDocumentSession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task<IEnumerable<CommentsPost>> GetAllCommentsPostsAsync()
        {
            return await _session.Query<CommentsPost>().ToListAsync();
        }

        public async Task<CommentsPost> GetCommentsPostByIdAsync(string commentsPostId)
        {
            return await _session.LoadAsync<CommentsPost>(commentsPostId);
        }

        public async Task<IEnumerable<CommentsPost>> GetCommentsPostsByPostIdAsync(string postId)
        {
            return await _session.Query<CommentsPost>().Where(cp => cp.PostId == postId).ToListAsync();
        }

        public async Task AddCommentsPostAsync(CommentsPost commentsPost)
        {
            await _session.StoreAsync(commentsPost);
            await _session.SaveChangesAsync();
        }

        public async Task UpdateCommentsPostAsync(CommentsPost commentsPost)
        {
            var existingCommentsPost = await _session.LoadAsync<CommentsPost>(commentsPost.Id);
            if (existingCommentsPost != null)
            {
                existingCommentsPost.Content = commentsPost.Content;
                existingCommentsPost.UserId = commentsPost.UserId;
                existingCommentsPost.CreatedAt = commentsPost.CreatedAt;
                await _session.SaveChangesAsync();
            }
        }

        public async Task DeleteCommentsPostAsync(string commentsPostId)
        {
            var commentsPost = await GetCommentsPostByIdAsync(commentsPostId);
            if (commentsPost != null)
            {
                _session.Delete(commentsPost);
                await _session.SaveChangesAsync();
            }
        }
    }
}