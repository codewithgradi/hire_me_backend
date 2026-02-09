using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
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
    .HasForeignKey<UserProfile>(p => p.AppUserId)
    .OnDelete(DeleteBehavior.Cascade);

    List<IdentityRole> roles = new List<IdentityRole>
  {
    new IdentityRole
    {
      Id="fab4fac1-c546-41de-aebc-a14da401b7e4",
      Name="ADMIN",
      NormalizedName= "ADMIN"
    },
    new IdentityRole
    {
      Id="c7b013f0-5201-4317-abd8-c211f91b7330",
      Name="USER",
      NormalizedName= "USER"
    }
  };
    builder.Entity<IdentityRole>().HasData(roles);
    builder.Entity<UserProfile>().Property(p => p.Qualification).HasConversion<string>();
  }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    // This tells EF Core: "I know there are changes, just let me run the command anyway"
    optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
  }
  public DbSet<UserProfile> UserProfiles { get; set; }


}
