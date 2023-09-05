using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<UserLink> UserLinks { get; set; }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .HasMany(x => x.UserLinks)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);


            base.OnModelCreating(builder);
        }
    }
}