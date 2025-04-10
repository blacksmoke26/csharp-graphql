// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Database.Seeders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations;

public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre> {
  public void Configure(EntityTypeBuilder<Genre> builder) {
    builder.ToTable("genres");
    
    builder.HasKey(e => e.Id).HasName("genres_pkey");

    builder.Property(e => e.Id).HasComment("ID");
    builder.Property(e => e.MovieId).HasComment("Movie");
    builder.Property(e => e.Name).HasComment("Name");

    builder.HasOne(d => d.Movie).WithMany(p => p.Genres)
      .HasConstraintName("FK_genres_movie_id_movies_id");
    
    builder.SeedData();
  }
}