using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Linq; // Adicione esta diretiva
using System.Collections.Generic;
using System.Threading.Tasks;
using Raven.Client.Documents;

namespace BlogApi.Data.Repositories
{
    public class LikePostRepository(IAsyncDocumentSession session) : ILikePostRepository
    {
        public async Task<IEnumerable<LikePost>> GetAllLikePostsAsync()
        {
            return await session.Query<LikePost>().ToListAsync();
        }

        public async Task<LikePost> GetLikePostByIdAsync(string userId, string postId)
        {
            return await session.LoadAsync<LikePost>($"{userId}_{postId}");
        }

        public async Task AddLikePostAsync(LikePost likePost)
        {
            await session.StoreAsync(likePost, $"{likePost.UserId}_{likePost.PostId}");
            await session.SaveChangesAsync();
        }

        public async Task UpdateLikePostAsync(LikePost likePost)
        {
            var existingLikePost = await session.LoadAsync<LikePost>($"{likePost.UserId}_{likePost.PostId}");
            if (existingLikePost != null)
            {
                existingLikePost.Post = likePost.Post;
                await session.SaveChangesAsync();
            }
        }

        public async Task DeleteLikePostAsync(string userId, string postId)
        {
            var likePost = await GetLikePostByIdAsync(userId, postId);
            if (likePost != null)
            {
                session.Delete(likePost);
                await session.SaveChangesAsync();
            }
        }
    }
}