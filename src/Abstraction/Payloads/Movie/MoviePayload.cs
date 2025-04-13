// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Account;
using Abstraction.Payloads.Genre;
using Abstraction.Payloads.Rating;

namespace Abstraction.Payloads.Movie;

[GraphQLName("Movie")]
public record MoviePayload {
  private float _rating;

  [GraphQLDescription("The id of movie")] [ID] [IsProjected(true)]
  public long? Id { get; set; }

  [GraphQLDescription("The owner id of movie")] [ID] [IsProjected(true)]
  public long? UserId { get; set; }

  [GraphQLDescription("The title of movie")]
  public string? Title { get; set; }

  [GraphQLDescription("The year of release")]
  public short? YearOfRelease { get; set; }

  [GraphQLDescription("The slug of movie")]
  public string? Slug { get; set; }

  [GraphQLDescription("Average rating")]
  public float? Rating {
    get => _rating;
    set => _rating = float.Round(value ?? 0, 1);
  }

  [GraphQLDescription("User's rating")]
  public short? UserRating { get; set; }

  [GraphQLDescription("The status of movie")]
  public string? Status { get; set; }

  [GraphQLDescription("The movie created timestamp")]
  public DateTime? CreatedAt { get; set; }

  [GraphQLDescription("The movie updated timestamp")]
  public DateTime? UpdatedAt { get; set; }

  [GraphQLDescription("The creator of movie")]
  public UserPayload? User { get; set; }

  [GraphQLDescription("The genres of movie")]
  public IEnumerable<GenrePayload> Genres { get; set; } = [];

  [GraphQLDescription("The ratings of movie")]
  public IEnumerable<RatingPayload> Ratings { get; set; } = [];
}