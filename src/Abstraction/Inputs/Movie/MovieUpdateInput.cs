// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Abstraction.Inputs.Movie;

[GraphQLDescription("An input use to create a movie")]
public struct MovieUpdateInput {
  [ID]
  public long Id { get; set; }

  public string Title { get; set; }
  public short YearOfRelease { get; set; }
  public IEnumerable<string> Genres { get; set; }
}