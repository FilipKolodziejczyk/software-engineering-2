using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class OrderDetailsModelEntityConfiguration : IEntityTypeConfiguration<OrderDetailsModel> {
    public void Configure(EntityTypeBuilder<OrderDetailsModel> builder) {
        builder.HasKey(x => x.CurrentOrderId);
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();

        builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
        builder.HasOne(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductId);
    }
}
