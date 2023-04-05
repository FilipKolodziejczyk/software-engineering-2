using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class OrderModelEntityConfiguration : IEntityTypeConfiguration<OrderModel> {
    public void Configure(EntityTypeBuilder<OrderModel> builder) {
        builder.HasKey(x => x.OrderID);
        builder.Property(x => x.ClientID).IsRequired();
        builder.Property(x => x.AddressID).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasMany(x => x.OrderDetails).WithOne(x => x.Order).HasForeignKey(x => x.OrderID);
        builder.HasMany(x => x.Complaints).WithOne(x => x.Order).HasForeignKey(x => x.ComplaintID);
    }
}
