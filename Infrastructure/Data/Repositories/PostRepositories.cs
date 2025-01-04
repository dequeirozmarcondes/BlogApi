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
    public class PostRepository : IPostRepository
    {
        private readonly IAsyncDocumentSession _session;

        public PostRepository(IAsyncDocumentSession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _session.Query<Post>().ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(string id)
        {
            return await _session.LoadAsync<Post>(id);
        }

        public async Task AddPostAsync(Post post)
        {
            await _session.StoreAsync(post);
            await _session.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            var existingPost = await _session.LoadAsync<Post>(post.Id);
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                existingPost.CommentsPosts = post.CommentsPosts;
                existingPost.LikePosts = post.LikePosts;
                await _session.SaveChangesAsync();
            }
        }

        public async Task DeletePostAsync(string id)
        {
            var post = await GetPostByIdAsync(id);
            if (post != null)
            {
                _session.Delete(post);
                await _session.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId)
        {
            return await _session.Query<Post>()
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}