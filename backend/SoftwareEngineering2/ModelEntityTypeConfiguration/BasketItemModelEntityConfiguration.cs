using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class BasketItemModelEntityConfiguration : IEntityTypeConfiguration<BasketItemModel> {
    public void Configure(EntityTypeBuilder<BasketItemModel> builder) {
        builder.HasKey(x => x.BasketItemId);
        builder.Property(x => x.ClientId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();

        builder.HasOne(x => x.Client).WithMany(x => x.BasketItems).HasForeignKey(x => x.ClientId).IsRequired(false);
        builder.HasOne(x => x.Product).WithMany(x => x.BasketItems).HasForeignKey(x => x.ProductId);
    }
}
