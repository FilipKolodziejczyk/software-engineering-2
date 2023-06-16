using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class AddressModelEntityConfiguration : IEntityTypeConfiguration<AddressModel> {
    public void Configure(EntityTypeBuilder<AddressModel> builder) {
        builder.HasKey(x => x.AddressId);
        builder.Property(x => x.Street).IsRequired();
        builder.Property(x => x.BuildingNo).IsRequired();
        builder.Property(x => x.HouseNo).IsRequired();
        builder.Property(x => x.City).IsRequired();
        builder.Property(x => x.PostalCode).IsRequired();
        builder.Property(x => x.Country).IsRequired();
    }
}
