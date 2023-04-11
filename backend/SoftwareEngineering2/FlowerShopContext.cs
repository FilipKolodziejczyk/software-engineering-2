using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2;

public class FlowerShopContext : DbContext {
    public FlowerShopContext(DbContextOptions<FlowerShopContext> options) : base(options) { }

    public DbSet<SampleModel> SampleModels { get; set; }
    public DbSet<SampleModelType> SampleModelTypes { get; set; }
    public DbSet<AddressModel> AddressModels { get; set; }
    public DbSet<ClientModel> ClientModels { get; set; }
    public DbSet<ComplaintModel> ComplaintModels { get; set; }
    public DbSet<DeliveryManModel> DeliveryManModels { get; set; }
    public DbSet<EmployeeModel> EmployeeModels { get; set; }
    public DbSet<OrderDetailsModel> OrderDetailsModels { get; set; }
    public DbSet<OrderModel> OrderModels { get; set; }
    public DbSet<ProductModel> ProductModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowerShopContext).Assembly);
        
        modelBuilder.Entity<SampleModelType>().HasData(
            new SampleModelType { Id = 1, Name = "Type 1" },
            new SampleModelType { Id = 2, Name = "Type 2" },
            new SampleModelType { Id = 3, Name = "Type 3" }
        );
    }
}