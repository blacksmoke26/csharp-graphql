// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Movie;

namespace Abstraction.Payloads.Rating;

[GraphQLName("Rating")]
public struct RatingPayload {
  [GraphQLDescription("The id of movie")] [ID]
  public long Id { get; set; }

  [GraphQLDescription("The id of user rated")]
  public long UserId { get; set; }

  [GraphQLDescription("The id of movie")]
  public long MovieId { get; set; }

  [GraphQLDescription("Overall score")]
  public short Score { get; set; }

  [GraphQLDescription("Additional feedback")]
  public string Feedback { get; set; }

  [GraphQLDescription("The rating created timestamp")]
  public DateTime CreatedAt { get; set; }

  [GraphQLDescription("The rating updated timestamp")]
  public DateTime UpdatedAt { get; set; }

  [GraphQLDescription("The movie associated with this rating")]
  public MoviePayload Movie { get; set; }
}