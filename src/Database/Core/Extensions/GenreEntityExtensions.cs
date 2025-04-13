// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Genre;

namespace Database.Core.Extensions;

/// <summary>
/// The class represents the extensions methods for 'Genre' entity
/// </summary>
public static class GenreEntityExtensions {
  public static IQueryable<GenrePayload> SelectGenrePayload(this IQueryable<Genre> source) {
    return source.Select(g => new GenrePayload {
      Id = g.Id,
      MovieId = g.MovieId,
      Name = g.Name,
    });
  }
}