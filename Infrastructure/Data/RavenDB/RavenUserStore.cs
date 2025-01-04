using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents.Session;
using BlogApi.Core.Entities;
using System.Threading;
using System.Threading.Tasks;
using Raven.Client.Documents;

namespace BlogApi.Infrastructure.Data.RavenDB
{
    public class RavenUserStore(IAsyncDocumentSession session) : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
    {
        private readonly IAsyncDocumentSession _session = session ?? throw new ArgumentNullException(nameof(session));

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            await _session.StoreAsync(user, cancellationToken);
            await _session.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            _session.Delete(user);
            await _session.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _session.LoadAsync<ApplicationUser>(userId, cancellationToken);
        }

        public async Task<ApplicationUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _session.Query<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        }

        public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string? userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            await _session.StoreAsync(user, cancellationToken);
            await _session.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            // No need to dispose anything here
            GC.SuppressFinalize(this);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string?> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(user);

            return Task.FromResult(user.PasswordHash != null);
        }
    }
}