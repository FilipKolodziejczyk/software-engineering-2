using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2; 

public class FlowerShopContext : DbContext {
    public FlowerShopContext(DbContextOptions<FlowerShopContext> options) : base(options) { }
    
    public DbSet<SampleModel> SampleModels { get; set; }
    public DbSet<SampleModelType> SampleModelTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowerShopContext).Assembly);
        
        modelBuilder.Entity<SampleModelType>().HasData(
            new SampleModelType { Id = 1, Name = "Type 1" },
            new SampleModelType { Id = 2, Name = "Type 2" },
            new SampleModelType { Id = 3, Name = "Type 3" }
        );
    }
}