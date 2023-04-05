using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class EmployeeModelEntityConfiguration : IEntityTypeConfiguration<EmployeeModel> {
    public void Configure(EntityTypeBuilder<EmployeeModel> builder) {
        builder.HasKey(x => x.EmployeeID);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        builder.HasMany(x => x.Complaints).WithOne(x => x.Employee).HasForeignKey(x => x.ComplaintID);
    }
}
