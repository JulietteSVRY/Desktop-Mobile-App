using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RickAndMorty.Model.Entity;

namespace RickAndMorty.Infrastructure.Database;

public sealed class RickAndMortyDbContext : DbContext
{
    public DbSet<Liked> Likeds { get; set; }
    public DbSet<User> Users { get; set; }

    public RickAndMortyDbContext(DbContextOptions<RickAndMortyDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
}

public class BloggingContextFactory : IDesignTimeDbContextFactory<RickAndMortyDbContext>
{
    public RickAndMortyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RickAndMortyDbContext>();
        optionsBuilder.UseSqlite("Data Source=blog.db");

        return new RickAndMortyDbContext(optionsBuilder.Options);
    }
}