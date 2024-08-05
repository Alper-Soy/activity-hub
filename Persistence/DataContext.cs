using Domain.features.activity;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; set; }
}
