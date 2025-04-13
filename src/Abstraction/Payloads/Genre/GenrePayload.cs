// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Abstraction.Payloads.Genre;

[GraphQLName("Genre")]
[GraphQLDescription("The movie genre")]
public struct GenrePayload {
  [GraphQLDescription("The id of genre")] [ID]
  public long? Id { get; set; }

  [GraphQLDescription("The movie id of genre")]
  public long? MovieId { get; set; }

  [GraphQLDescription("The name of genre")]
  public string? Name { get; set; }
}