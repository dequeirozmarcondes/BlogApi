using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace BlogApi.Infrastructure.Data.Repositories
{
    public class ApplicationUserRepository(IAsyncDocumentSession session) : IApplicationUserRepository
    {
        private readonly IAsyncDocumentSession _session = session ?? throw new ArgumentNullException(nameof(session));

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _session.LoadAsync<ApplicationUser>(userId);
        }

        public async Task<ApplicationUser?> GetUserByUserNameAsync(string userName)
        {
            return await _session.Query<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _session.Query<ApplicationUser>().ToListAsync();
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            await _session.StoreAsync(user);
            await _session.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            var existingUser = await GetUserByIdAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.Bio = user.Bio;
                existingUser.Posts = user.Posts;
                existingUser.LikePosts = user.LikePosts;
                existingUser.CommentsPosts = user.CommentsPosts;
                await _session.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                _session.Delete(user);
                await _session.SaveChangesAsync();
            }
        }
    }
}