using BlogApi.Models;
using Raven.Client.Documents;
using static BlogApi.Models.IPostRepositories;

namespace BlogApi.Data
{
    public class Repositories : IPostRepositories
    {
        public class PostRepository : IPostRepository
        {
            private readonly IDocumentStore _documentStore;

            public PostRepository(IDocumentStore documentStore)
            {
                _documentStore = documentStore;
            }

            public async Task<IEnumerable<Post>> GetAllPostsAsync()
            {
                using (var session = _documentStore.OpenAsyncSession())
                {
                    return await session.Query<Post>().ToListAsync();
                }
            }

            public async Task<Post> GetPostByIdAsync(string id)
            {
                using (var session = _documentStore.OpenAsyncSession())
                {
                    return await session.LoadAsync<Post>(id);
                }
            }

            public async Task AddPostAsync(Post post)
            {
                using (var session = _documentStore.OpenAsyncSession())
                {
                    await session.StoreAsync(post);
                    await session.SaveChangesAsync();
                }
            }

            public async Task UpdatePostAsync(Post post)
            {
                using (var session = _documentStore.OpenAsyncSession())
                {
                    var existingPost = await session.LoadAsync<Post>(post.Id);
                    if (existingPost != null)
                    {
                        existingPost.Title = post.Title;
                        existingPost.Content = post.Content;
                        await session.SaveChangesAsync();
                    }
                }
            }

            public async Task DeletePostAsync(string id)
            {
                using (var session = _documentStore.OpenAsyncSession())
                {
                    session.Delete(id);
                    await session.SaveChangesAsync();
                }
            }
        }
    }
}
