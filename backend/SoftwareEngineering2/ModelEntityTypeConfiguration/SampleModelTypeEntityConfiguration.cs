using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration; 

public class SampleModelTypeEntityConfiguration : IEntityTypeConfiguration<SampleModelType> {
    public void Configure(EntityTypeBuilder<SampleModelType> builder) {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
    }
}