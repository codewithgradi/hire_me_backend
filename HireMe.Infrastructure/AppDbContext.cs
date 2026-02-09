using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace HireMe.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser>
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.Entity<AppUser>()
    .HasOne(x => x.UserProfile)
    .WithOne(x => x.AppUser)
    .HasForeignKey<UserProfile>(p => p.AppUserId);
  }
  public DbSet<UserProfile> UserProfiles { get; set; }
}
