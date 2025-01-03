using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using BlogApi.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public class CommentsPostService(ICommentsPostRepository commentsPostRepository) : ICommentsPostService
    {

        // Retorna todos os comentários dos posts
        public async Task<IEnumerable<CommentsPost>> GetAllCommentsPostsAsync()
        {
            return await commentsPostRepository.GetAllCommentsPostsAsync();
        }

        // Retorna um comentário específico pelo ID
        public async Task<CommentsPost> GetCommentsPostByIdAsync(string commentsPostId)
        {
            return await commentsPostRepository.GetCommentsPostByIdAsync(commentsPostId);
        }

        // Adiciona um novo comentário no post
        public async Task AddCommentsPostAsync(CommentsPost commentsPost)
        {
            await commentsPostRepository.AddCommentsPostAsync(commentsPost);
        }

        // Atualiza um comentário existente no post
        public async Task UpdateCommentsPostAsync(CommentsPost commentsPost)
        {
            await commentsPostRepository.UpdateCommentsPostAsync(commentsPost);
        }

        // Deleta um comentário no post pelo ID
        public async Task DeleteCommentsPostAsync(string commentsPostId)
        {
            await commentsPostRepository.DeleteCommentsPostAsync(commentsPostId);
        }
    }
}