using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class ProductModelEntityConfiguration : IEntityTypeConfiguration<ProductModel> {
    public void Configure(EntityTypeBuilder<ProductModel> builder) {
        builder.HasKey(x => x.ProductId);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Archived).IsRequired();
    }
}
