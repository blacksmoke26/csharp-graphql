// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Database.Core.Base;

namespace Database.Entities;

[Table("genres")]
[Index("Name", "MovieId", Name = "UNQ_movie_name_genre", IsUnique = true)]
public partial class Genre : EntityBase {
  /// <summary>ID</summary>
  [Key, Column("id")]
  public long Id { get; set; }

  /// <summary>Movie</summary>
  [Column("movie_id")]
  public long MovieId { get; set; }

  /// <summary>Name</summary>
  [Column("name"), StringLength(60)]
  public string Name { get; set; } = null!;

  [ForeignKey("MovieId"), InverseProperty("Genres")]
  public virtual Movie Movie { get; set; } = null!;
}