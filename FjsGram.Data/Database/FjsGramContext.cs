using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FjsGram.Data.Database;

public class FjsGramContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Create(modelBuilder.Entity<Image>());
        Create(modelBuilder.Entity<User>());
        Create(modelBuilder.Entity<Post>());
    }

    private void Create(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength(255);
        builder.Property(x => x.Title).HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(1024);
        builder.HasMany(x => x.Images).WithOne();
    }

    private static void Create(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Login);
        builder.Property(x => x.Login).HasMaxLength(255);
        builder.Property(x => x.Email).HasMaxLength(255);
        builder.Property(x => x.PasswordHash).HasMaxLength(1024);
        builder.HasMany(x => x.Posts).WithOne(p => p.Author);
    }

    private static void Create(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Data);
    }
}
