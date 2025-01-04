using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Infrastructure.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly IAsyncDocumentSession _session;

        public ApplicationUserRepository(IAsyncDocumentSession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _session.LoadAsync<ApplicationUser>(userId);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
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
            await _session.StoreAsync(user);
            await _session.SaveChangesAsync();
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