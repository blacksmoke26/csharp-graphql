// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Database.Seeders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations;

public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie> {
  public void Configure(EntityTypeBuilder<Movie> builder) {
    builder.ToTable("movies");

    builder.HasKey(e => e.Id).HasName("movies_pkey");

    builder.Property(e => e.Id).HasComment("ID");
    builder.Property(e => e.CreatedAt).HasComment("Created");
    builder.Property(e => e.Slug).HasComment("Slug");
    builder.Property(e => e.Status).HasComment("Status");
    builder.Property(e => e.Title).HasComment("Title");
    builder.Property(e => e.UpdatedAt).HasComment("Updated");
    builder.Property(e => e.UserId).HasComment("User");
    builder.Property(e => e.YearOfRelease).HasComment("Year of Release");

    builder.HasOne(d => d.User).WithMany(p => p.Movies)
      .HasConstraintName("FK_movies_user_id_users_id");
    
    builder.SeedData();
  }
}