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
    public class LikePostRepository : ILikePostRepository
    {
        private readonly IAsyncDocumentSession _session;

        public LikePostRepository(IAsyncDocumentSession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task<IEnumerable<LikePost>> GetAllLikePostsAsync()
        {
            return await _session.Query<LikePost>().ToListAsync();
        }

        public async Task<LikePost> GetLikePostByIdAsync(string userId, string postId)
        {
            return await _session.LoadAsync<LikePost>($"{userId}_{postId}");
        }

        public async Task<IEnumerable<LikePost>> GetLikePostsByPostIdAsync(string postId)
        {
            return await _session.Query<LikePost>().Where(lp => lp.PostId == postId).ToListAsync();
        }

        public async Task AddLikePostAsync(LikePost likePost)
        {
            await _session.StoreAsync(likePost, $"{likePost.UserId}_{likePost.PostId}");
            await _session.SaveChangesAsync();
        }

        public async Task UpdateLikePostAsync(LikePost likePost)
        {
            var existingLikePost = await _session.LoadAsync<LikePost>($"{likePost.UserId}_{likePost.PostId}");
            if (existingLikePost != null)
            {
                existingLikePost.Post = likePost.Post;
                await _session.SaveChangesAsync();
            }
        }

        public async Task DeleteLikePostAsync(string userId, string postId)
        {
            var likePost = await GetLikePostByIdAsync(userId, postId);
            if (likePost != null)
            {
                _session.Delete(likePost);
                await _session.SaveChangesAsync();
            }
        }
    }
}