using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace BlogApi.Infrastructure.Data.Repositories
{
    public class PostRepository(IAsyncDocumentSession session) : IPostRepository
    {
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await session.Query<Post>().ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(string id)
        {
            return await session.LoadAsync<Post>(id);
        }

        public async Task AddPostAsync(Post post)
        {
            await session.StoreAsync(post);
            await session.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            var existingPost = await session.LoadAsync<Post>(post.Id);
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                existingPost.CommentsPosts = post.CommentsPosts;
                existingPost.LikePosts = post.LikePosts;
                await session.SaveChangesAsync();
            }
        }

        public async Task DeletePostAsync(string id)
        {
            var post = await GetPostByIdAsync(id);
            if (post != null)
            {
                session.Delete(post);
                await session.SaveChangesAsync();
            }
        }
    }
}