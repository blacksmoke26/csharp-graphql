// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Database.Seeders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations;

public class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating> {
  public void Configure(EntityTypeBuilder<Rating> builder) {
    builder.ToTable("ratings");

    builder.HasKey(e => e.Id).HasName("ratings_pkey");

    builder.Property(e => e.Id).HasComment("ID");
    builder.Property(e => e.CreatedAt).HasComment("Created");
    builder.Property(e => e.Feedback).HasComment("Feedback");
    builder.Property(e => e.MovieId).HasComment("Movie");
    builder.Property(e => e.Score).HasDefaultValue((short)1).HasComment("Score");
    builder.Property(e => e.UpdatedAt).HasComment("Updated");
    builder.Property(e => e.UserId).HasComment("User");

    builder.HasOne(d => d.Movie).WithMany(p => p.Ratings)
      .HasConstraintName("FK_ratings_movie_id_movies_id");
    builder.HasOne(d => d.User).WithMany(p => p.Ratings)
      .HasConstraintName("FK_ratings_user_id_users_id");

    builder.SeedData();
  }
}