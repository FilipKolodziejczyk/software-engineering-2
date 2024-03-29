using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2;

public class FlowerShopContext : DbContext {
    public FlowerShopContext(DbContextOptions<FlowerShopContext> options) : base(options) { }

    public DbSet<AddressModel> AddressModels { get; set; } = null!;
    public DbSet<ClientModel> ClientModels { get; set; } = null!;
    public DbSet<ComplaintModel> ComplaintModels { get; set; } = null!;
    public DbSet<DeliveryManModel> DeliveryManModels { get; set; } = null!;
    public DbSet<EmployeeModel> EmployeeModels { get; set; } = null!;
    public DbSet<OrderDetailsModel> OrderDetailsModels { get; set; } = null!;
    public DbSet<OrderModel> OrderModels { get; set; } = null!;
    public DbSet<ProductModel> ProductModels { get; set; } = null!;
    public DbSet<BasketItemModel> BasketItemModels { get; set; } = null!;
    public DbSet<ImageModel> ImageModels { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowerShopContext).Assembly);

        // TODO: temporary solution (password: password)
        modelBuilder.Entity<EmployeeModel>().HasData(new EmployeeModel {
            Email = "admin@flowershop.com",
            Name = "Władysław Howalski",
            Password = "AQAAAAIAAYagAAAAEHiYiXUCLpBDCy3l60OqSPW+GNZExxF4PwXI8VtkhKZqjVsMFdhw68orF475JKPXkA==",
            EmployeeId = 1
        });
    }
}