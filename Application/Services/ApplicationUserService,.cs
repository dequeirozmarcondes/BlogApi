using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Application.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(IApplicationUserRepository userRepository, IPostRepository postRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Posts = (ICollection<Post>)await _postRepository.GetPostsByUserIdAsync(userId);
            }
            return user;
        }

        public async Task<ApplicationUser?> GetUserByUserNameAsync(string userName)
        {
            var user = await _userRepository.GetUserByUserNameAsync(userName);
            if (user != null)
            {
                user.Posts = (ICollection<Post>)await _postRepository.GetPostsByUserIdAsync(user.Id);
            }
            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            foreach (var user in users)
            {
                user.Posts = (ICollection<Post>)await _postRepository.GetPostsByUserIdAsync(user.Id);
            }
            return users;
        }

        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userRepository.AddUserAsync(user);
            }
            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (existingUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            // Atualiza apenas os campos fornecidos, preservando as propriedades existentes
            existingUser.UserName = user.UserName ?? existingUser.UserName;
            existingUser.Email = user.Email ?? existingUser.Email;

            var result = await _userManager.UpdateAsync(existingUser);
            if (result.Succeeded)
            {
                await _userRepository.UpdateUserAsync(existingUser);
            }
            return result;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                await _userRepository.DeleteUserAsync(userId);
            }
        }
    }
}