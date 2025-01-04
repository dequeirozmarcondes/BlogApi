using BlogApi.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Application.IServices
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IdentityResult> AddUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);
    }
}