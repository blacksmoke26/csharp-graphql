// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

namespace Database.Context;

public partial class ApplicationDbContext {
  /// <summary>Users entity object</summary>
  public virtual DbSet<User> Users { get; set; }

  /// <summary>Movies entity object</summary>
  public virtual DbSet<Movie> Movies { get; set; }

  /// <summary>Ratings entity object</summary>
  public virtual DbSet<Rating> Ratings { get; set; }

  /// <summary>Genres entity object</summary>
  public virtual DbSet<Genre> Genres { get; set; }
}