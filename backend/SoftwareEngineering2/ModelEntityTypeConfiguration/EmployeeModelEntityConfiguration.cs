using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class EmployeeModelEntityConfiguration : IEntityTypeConfiguration<EmployeeModel> {
    public void Configure(EntityTypeBuilder<EmployeeModel> builder) {
        builder.HasKey(x => x.EmployeeId);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
    }
}
