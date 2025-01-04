using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;

namespace BlogApi.Application.Services
{
    public class LikePostService(ILikePostRepository likePostRepository) : ILikePostService
    {

        // Retorna todos os likes nos posts
        public async Task<IEnumerable<LikePost>> GetAllLikePostsAsync()
        {
            return await likePostRepository.GetAllLikePostsAsync();
        }

        // Retorna um like específico pelo ID do usuário e ID do post
        public async Task<LikePost> GetLikePostByIdAsync(string userId, string postId)
        {
            return await likePostRepository.GetLikePostByIdAsync(userId, postId);
        }

        // Adiciona um novo like no post
        public async Task AddLikePostAsync(LikePost likePost)
        {
            await likePostRepository.AddLikePostAsync(likePost);
        }

        // Atualiza um like existente no post
        public async Task UpdateLikePostAsync(LikePost likePost)
        {
            await likePostRepository.UpdateLikePostAsync(likePost);
        }

        // Deleta um like no post pelo ID do usuário e ID do post
        public async Task DeleteLikePostAsync(string userId, string postId)
        {
            await likePostRepository.DeleteLikePostAsync(userId, postId);
        }
    }
}