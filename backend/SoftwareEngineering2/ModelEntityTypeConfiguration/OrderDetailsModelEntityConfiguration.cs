using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class OrderDetailsModelEntityConfiguration : IEntityTypeConfiguration<OrderDetailsModel> {
    public void Configure(EntityTypeBuilder<OrderDetailsModel> builder) {
        builder.HasKey(x => x.CurrentOrderId);
        builder.Property(x => x.Quantity).IsRequired();
    }
}
