using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration; 

public class SampleModelEntityConfiguration : IEntityTypeConfiguration<SampleModel> {
    public void Configure(EntityTypeBuilder<SampleModel> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.HasOne(x => x.Type).WithMany().HasForeignKey(x => x.Id);
    }
}