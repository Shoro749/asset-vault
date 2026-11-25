using System.Reflection;
using Domain.Asset;
using Domain.Category;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Asset> Assets { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<AssetCategory> AssetCategories { get; init; }
    public DbSet<PreviewImage> PreviewImages { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}