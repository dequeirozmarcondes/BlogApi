using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Raven.Client.Documents.Session;
using BlogApi.Core.Entities;

namespace BlogApi.Infrastructure.Data.DbContextEntityFrameworkCore
{
    public class ApplicationDbContext(IAsyncDocumentSession session) : IdentityDbContext<ApplicationUser>
    {
        private readonly IAsyncDocumentSession _session = session ?? throw new ArgumentNullException(nameof(session));

        public DbSet<Post> Posts { get; set; }
        public DbSet<LikePost> LikePosts { get; set; }
        public DbSet<CommentsPost> CommentsPosts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Use RavenDB session SaveChangesAsync method instead.");
        }

        public async Task SaveChangesWithRavenDbAsync()
        {
            await _session.SaveChangesAsync();
        }
    }
}