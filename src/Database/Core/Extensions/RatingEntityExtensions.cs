// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Rating;

namespace Database.Core.Extensions;

/// The class represents the extensions methods for 'Rating' entity
public static class RatingEntityExtensions {
  public static IQueryable<RatingPayload> SelectRatingPayload(this IQueryable<Rating> source) {
    return source.Select(r => new RatingPayload {
      Id = r.Id,
      MovieId = r.MovieId,
      Score = r.Score,
      Feedback = r.Feedback,
      CreatedAt = r.CreatedAt,
      UpdatedAt = r.UpdatedAt,
    });
  }
}