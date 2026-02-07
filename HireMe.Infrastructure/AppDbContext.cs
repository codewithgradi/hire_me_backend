using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HireMe.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser>
{
  public AppDbContext(DbContextOptions options) : base(options)
  { }
  public DbSet<UserProfile> UserProfiles { get; set; }
}
