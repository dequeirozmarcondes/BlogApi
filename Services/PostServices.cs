using BlogApi.Models;
using static BlogApi.Models.IPostRepositories;

namespace BlogApi.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<Post> GetPostByIdAsync(string id)
        {
            return await _postRepository.GetPostByIdAsync(id);
        }

        public async Task AddPostAsync(Post post)
        {
            await _postRepository.AddPostAsync(post);
        }

        public async Task UpdatePostAsync(Post post)
        {
            await _postRepository.UpdatePostAsync(post);
        }

        public async Task DeletePostAsync(string id)
        {
            await _postRepository.DeletePostAsync(id);
        }
    }
}
