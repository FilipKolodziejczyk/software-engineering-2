using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class OrderModelEntityConfiguration : IEntityTypeConfiguration<OrderModel> {
    public void Configure(EntityTypeBuilder<OrderModel> builder) {
        builder.HasKey(x => x.OrderId);
        builder.Property(x => x.ClientId).IsRequired();
        builder.Property(x => x.AddressId).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasMany(x => x.OrderDetails).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
        builder.HasMany(x => x.Complaints).WithOne(x => x.Order).HasForeignKey(x => x.ComplaintId);

        builder.HasOne(x => x.Address).WithMany(x => x.Orders).HasForeignKey(x => x.AddressId);
        builder.HasOne(x => x.DeliveryMan).WithMany(x => x.Orders).HasForeignKey(x => x.DeliveryManId).IsRequired(false);
        builder.HasOne(x => x.Client).WithMany(x => x.Orders).HasForeignKey(x => x.ClientId);
    }
}
