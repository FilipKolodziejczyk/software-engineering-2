using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class ProductModelEntityConfiguration : IEntityTypeConfiguration<ProductModel> {
    public void Configure(EntityTypeBuilder<ProductModel> builder) {
        builder.HasKey(x => x.ProductID);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Archived).IsRequired();

        builder.HasMany(x => x.OrderDetails).WithOne(x => x.Product).HasForeignKey(x => x.ProductID);
        builder.HasMany(x => x.BasketItems).WithOne(x => x.Product).HasForeignKey(x => x.ProductID);
    }
}
