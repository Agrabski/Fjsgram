using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FjsGram.Data.Database;

public class DesignTimeFactory : IDesignTimeDbContextFactory<FjsGramContext>
{
    public FjsGramContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<FjsGramContext>();
        builder.UseSqlServer();
        return new(builder.Options);
    }
}
