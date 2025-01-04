using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Application.Services
{
    public class LikePostService : ILikePostService
    {
        private readonly ILikePostRepository _likePostRepository;

        public LikePostService(ILikePostRepository likePostRepository)
        {
            _likePostRepository = likePostRepository ?? throw new ArgumentNullException(nameof(likePostRepository));
        }

        public async Task<IEnumerable<LikePost>> GetAllLikePostsAsync()
        {
            return await _likePostRepository.GetAllLikePostsAsync();
        }

        public async Task<LikePost?> GetLikePostByIdAsync(string userId, string postId)
        {
            return await _likePostRepository.GetLikePostByIdAsync(userId, postId);
        }

        public async Task AddLikePostAsync(LikePost likePost)
        {
            await _likePostRepository.AddLikePostAsync(likePost);
        }

        public async Task UpdateLikePostAsync(LikePost likePost)
        {
            await _likePostRepository.UpdateLikePostAsync(likePost);
        }

        public async Task DeleteLikePostAsync(string userId, string postId)
        {
            await _likePostRepository.DeleteLikePostAsync(userId, postId);
        }
    }
}