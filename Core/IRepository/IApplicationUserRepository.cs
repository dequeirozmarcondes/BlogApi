using BlogApi.Core.Entities;

namespace BlogApi.Core.IRepository
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser?> GetUserByIdAsync(string userId);
        Task<ApplicationUser?> GetUserByUserNameAsync(string userName);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);
    }
}