using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new { aa.ActivityId, aa.UserId }));

        builder.Entity<ActivityAttendee>().HasOne(u => u.User).WithMany(a => a.Activities)
            .HasForeignKey(aa => aa.UserId);

        builder.Entity<ActivityAttendee>().HasOne(a => a.Activity).WithMany(a => a.Attendees)
            .HasForeignKey(aa => aa.ActivityId);

        builder.Entity<Comment>().HasOne(a => a.Activity).WithMany(a => a.Comments).OnDelete(DeleteBehavior.Cascade);
    }
}
