using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace BlogApi.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        }

        // Retorna todos os posts
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        // Retorna um post específico pelo ID
        public async Task<Post> GetPostByIdAsync(string id)
        {
            // Converter o ID recebido para o formato esperado pelo RavenDB
            string decodedId = HttpUtility.UrlDecode(id);
            Console.WriteLine(decodedId);
            return await _postRepository.GetPostByIdAsync(decodedId);
        }

        // Adiciona um novo post
        public async Task AddPostAsync(Post post)
        {
            await _postRepository.AddPostAsync(post);
        }

        // Atualiza um post existente
        public async Task UpdatePostAsync(Post post)
        {
            await _postRepository.UpdatePostAsync(post);
        }

        // Deleta um post pelo ID
        public async Task DeletePostAsync(string id)
        {
            string decodedId = HttpUtility.UrlDecode(id);
            await _postRepository.DeletePostAsync(decodedId);
        }
    }
}