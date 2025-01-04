using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Application.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(IApplicationUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.GetUserByUserNameAsync(userName);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
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
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _userRepository.UpdateUserAsync(user);
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