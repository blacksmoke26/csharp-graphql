// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using System.Linq.Expressions;
using Abstraction.Payloads.Movie;

namespace Database.Core.Extensions;

/// The class represents the extensions methods for 'Movie' entity
public static class MovieEntityExtensions {
  /// <summary>
  /// Generates a slug against current values
  /// </summary>
  public static void GenerateSlug(this Movie movie) {
    movie.Slug = GenerateSlug(movie, movie.Title, movie.YearOfRelease);
  }

  /// <summary>
  /// Generates a slug based on a movie name and the year of release
  /// </summary>
  public static string GenerateSlug(this Movie movie, string title, short yearOfRelease) {
    return movie.Slug = StringHelper.Slugify(title, " ", yearOfRelease);
  }

  public static IQueryable<MoviePayload> SelectMoviePayload(this IQueryable<Movie> source) {
    return source.Select(movie => new MoviePayload {
      Id = movie.Id,
      UserId = movie.UserId,
      Title = movie.Title,
      Rating = (float)movie.Ratings.Average(z => z.Score),
      YearOfRelease = movie.YearOfRelease,
      Status = movie.Status,
      Slug = movie.Slug,
      CreatedAt = movie.CreatedAt,
      UpdatedAt = movie.UpdatedAt,
    });
  }
}