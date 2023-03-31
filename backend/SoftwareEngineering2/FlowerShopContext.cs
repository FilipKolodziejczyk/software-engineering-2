using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2; 

public class FlowerShopContext : DbContext {
    public FlowerShopContext(DbContextOptions<FlowerShopContext> options) : base(options) { }
    
    public DbSet<SampleModel> SampleModels { get; set; }
    public DbSet<SampleModelType> SampleModelTypes { get; set; }
}