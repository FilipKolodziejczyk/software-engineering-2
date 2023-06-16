using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class OrderModelEntityConfiguration : IEntityTypeConfiguration<OrderModel> {
    public void Configure(EntityTypeBuilder<OrderModel> builder) {
        builder.HasKey(x => x.OrderId);
        builder.Property(x => x.Status).IsRequired();

        builder.HasOne(x => x.DeliveryMan).WithMany(x => x.Orders).IsRequired(false);
    }
}