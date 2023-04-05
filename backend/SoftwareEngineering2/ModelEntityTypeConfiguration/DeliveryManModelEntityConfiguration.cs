using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class DeliveryManModelEntityConfiguration : IEntityTypeConfiguration<DeliveryManModel> {
    public void Configure(EntityTypeBuilder<DeliveryManModel> builder) {
        builder.HasKey(x => x.DeliveryManID);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        builder.HasMany(x => x.Orders).WithOne(x => x.DeliveryMan).HasForeignKey(x => x.OrderID);
    }
}
