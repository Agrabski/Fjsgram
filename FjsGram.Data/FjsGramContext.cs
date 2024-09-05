using Microsoft.EntityFrameworkCore;

namespace FjsGram.Data;

public class FjsGramContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Post> Posts => Set<Post>();
}
