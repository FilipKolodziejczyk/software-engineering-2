﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class DeliveryManModelEntityConfiguration : IEntityTypeConfiguration<DeliveryManModel> {
    public void Configure(EntityTypeBuilder<DeliveryManModel> builder) {
        builder.HasKey(x => x.DeliveryManId);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
    }
}