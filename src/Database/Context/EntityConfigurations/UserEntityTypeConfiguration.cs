// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Database.Seeders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations;

internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User> {
  public void Configure(EntityTypeBuilder<User> builder) {
    builder.ToTable("users");

    builder.HasKey(e => e.Id).HasName("users_pkey");

    builder.HasIndex(e => e.Metadata, "IDX_users_metadata")
      .HasMethod("gin")
      .HasAnnotation("Npgsql:StorageParameter:gin_pending_list_limit", "2097151");

    builder.Property(e => e.Id).HasComment("ID");
    builder.Property(e => e.AuthKey).HasComment("Authorization Key");
    builder.Property(e => e.CreatedAt).HasComment("Created");
    builder.Property(e => e.Email).HasComment("Email Address");
    builder.Property(e => e.FirstName).HasComment("First name");
    builder.Property(e => e.LastName).HasComment("Last name");
    builder.Property(e => e.Metadata).HasDefaultValueSql("'{}'::jsonb").HasComment("Metadata");
    builder.Property(e => e.Password).HasComment("Password");
    builder.Property(e => e.PasswordHash).HasComment("Password Hash");
    builder.Property(e => e.Role).HasComment("Role");
    builder.Property(e => e.Status).HasComment("Status");
    builder.Property(e => e.UpdatedAt).HasComment("Updated");
    
    builder.SeedData();
  }
}