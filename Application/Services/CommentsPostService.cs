using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Application.Services
{
    public class CommentsPostService : ICommentsPostService
    {
        private readonly ICommentsPostRepository _commentsPostRepository;

        public CommentsPostService(ICommentsPostRepository commentsPostRepository)
        {
            _commentsPostRepository = commentsPostRepository ?? throw new ArgumentNullException(nameof(commentsPostRepository));
        }

        public async Task<IEnumerable<CommentsPost>> GetAllCommentsPostsAsync()
        {
            return await _commentsPostRepository.GetAllCommentsPostsAsync();
        }

        public async Task<CommentsPost> GetCommentsPostByIdAsync(string commentsPostId)
        {
            return await _commentsPostRepository.GetCommentsPostByIdAsync(commentsPostId);
        }

        public async Task AddCommentsPostAsync(CommentsPost commentsPost)
        {
            commentsPost.CreatedAt = DateTime.UtcNow;
            await _commentsPostRepository.AddCommentsPostAsync(commentsPost);
        }

        public async Task UpdateCommentsPostAsync(CommentsPost commentsPost)
        {
            await _commentsPostRepository.UpdateCommentsPostAsync(commentsPost);
        }

        public async Task DeleteCommentsPostAsync(string commentsPostId)
        {
            await _commentsPostRepository.DeleteCommentsPostAsync(commentsPostId);
        }
    }
}