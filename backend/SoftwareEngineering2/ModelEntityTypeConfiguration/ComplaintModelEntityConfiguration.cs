using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class ComplaintModelEntityConfiguration : IEntityTypeConfiguration<ComplaintModel> {
    public void Configure(EntityTypeBuilder<ComplaintModel> builder) {
        builder.HasKey(x => x.ComplaintId);
        builder.Property(x => x.ClientId).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.EmployeeId).IsRequired();
        builder.Property(x => x.Topic).IsRequired();
        builder.Property(x => x.Description).IsRequired();

        builder.HasOne(x => x.Order).WithMany(x => x.Complaints).HasForeignKey(x => x.OrderId);
        builder.HasOne(x => x.Client).WithMany(x => x.Complaints).HasForeignKey(x => x.ClientId);
        builder.HasOne(x => x.Employee).WithMany(x => x.Complaints).HasForeignKey(x => x.EmployeeId);
    }
}
