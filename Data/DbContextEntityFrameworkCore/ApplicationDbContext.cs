using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Raven.Client.Documents.Session;
using BlogApi.Core.Entities;

namespace BlogApi.Data.DbContextEntityFrameworkCore
{
    public class ApplicationDbContext(IAsyncDocumentSession session) : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<LikePost> LikePosts { get; set; }
        public DbSet<CommentsPost> CommentsPosts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Use RavenDB session SaveChangesAsync method instead.");
        }

        public async Task SaveChangesAsync()
        {
            await session.SaveChangesAsync();
        }
    }
}