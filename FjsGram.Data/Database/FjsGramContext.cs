using Microsoft.EntityFrameworkCore;

namespace FjsGram.Data.Database;

public class FjsGramContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
}
