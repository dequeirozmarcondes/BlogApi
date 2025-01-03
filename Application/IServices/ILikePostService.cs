﻿using BlogApi.Core.Entities;

namespace BlogApi.Application.IServices
{
    public interface ILikePostService
    {
        Task<IEnumerable<LikePost>> GetAllLikePostsAsync();
        Task<LikePost> GetLikePostByIdAsync(string userId, string postId);
        Task AddLikePostAsync(LikePost likePost);
        Task UpdateLikePostAsync(LikePost likePost);
        Task DeleteLikePostAsync(string userId, string postId);
    }
}