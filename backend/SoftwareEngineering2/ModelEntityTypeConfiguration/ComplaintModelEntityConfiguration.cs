using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class ComplaintModelEntityConfiguration : IEntityTypeConfiguration<ComplaintModel> {
    public void Configure(EntityTypeBuilder<ComplaintModel> builder) {
        builder.HasKey(x => x.ComplaintID);
        builder.Property(x => x.ClientID).IsRequired();
        builder.Property(x => x.OrderID).IsRequired();
        builder.Property(x => x.EmployeeID).IsRequired();
        builder.Property(x => x.Topic).IsRequired();
        builder.Property(x => x.Description).IsRequired();
    }
}
