﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.ModelEntityTypeConfiguration;

public class ClientModelEntityConfiguration : IEntityTypeConfiguration<ClientModel> {
    public void Configure(EntityTypeBuilder<ClientModel> builder) {
        builder.HasKey(x => x.ClientID);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.AddressID).IsRequired();
        builder.Property(x => x.HasNewsletterOn).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        // TDOO: fix schema, it should be working witout .OnDelete
        builder.HasMany(x => x.Complaints).WithOne(x => x.Client).HasForeignKey(x => x.ComplaintID).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Orders).WithOne(x => x.Client).HasForeignKey(x => x.OrderID).OnDelete(DeleteBehavior.NoAction);
    }
}
