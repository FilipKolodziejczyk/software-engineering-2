using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration; 

public class ImageModelEntityConfiguration : IEntityTypeConfiguration<ImageModel> {
    public void Configure(EntityTypeBuilder<ImageModel> builder) {
        builder.HasKey(x => x.ImageId);
        builder.Property(x => x.ImageUri).IsRequired();
        builder
            .HasMany(x => x.Products)
            .WithMany(x => x.Images);
    }
}