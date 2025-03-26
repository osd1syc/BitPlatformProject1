﻿using Project1.Server.Api.Models.Identity;

namespace Project1.Server.Api.Data.Configurations.Identity;

public class WebAuthnCredentialConfiguration : IEntityTypeConfiguration<WebAuthnCredential>
{
    public void Configure(EntityTypeBuilder<WebAuthnCredential> builder)
    {
        builder.HasOne(t => t.User)
            .WithMany(u => u.WebAuthnCredentials)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
