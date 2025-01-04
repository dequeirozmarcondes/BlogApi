using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;

namespace BlogApi.Application.Services
{
    public class PostService(IPostRepository postRepository) : IPostService
    {

        // Retorna todos os posts
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await postRepository.GetAllPostsAsync();
        }

        // Retorna um post específico pelo ID
        public async Task<Post> GetPostByIdAsync(string id)
        {
            return await postRepository.GetPostByIdAsync(id);
        }

        // Adiciona um novo post
        public async Task AddPostAsync(Post post)
        {
            await postRepository.AddPostAsync(post);
        }

        // Atualiza um post existente
        public async Task UpdatePostAsync(Post post)
        {
            await postRepository.UpdatePostAsync(post);
        }

        // Deleta um post pelo ID
        public async Task DeletePostAsync(string id)
        {
            await postRepository.DeletePostAsync(id);
        }
    }
}