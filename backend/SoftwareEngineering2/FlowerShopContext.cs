using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2;

public class FlowerShopContext : DbContext {
    public FlowerShopContext(DbContextOptions<FlowerShopContext> options) : base(options) { }

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

        // TODO: temporary solution (password: password)
        modelBuilder.Entity<EmployeeModel>().HasData(new EmployeeModel {
            Email = "admin@flowershop.com",
            Name = "W³adys³aw Howalski",
            Password = "AQAAAAIAAYagAAAAEHiYiXUCLpBDCy3l60OqSPW+GNZExxF4PwXI8VtkhKZqjVsMFdhw68orF475JKPXkA==",
            EmployeeID = 1
        });
    }
}