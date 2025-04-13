// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Movie;
using Application.Helpers;
using Database.Context;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class MovieService(
  MovieRepository movieRepo,
  GenreRepository genreRepo,
  ApplicationDbContext dbContext
) : ServiceBase {
  public async Task<Movie?> CreateAsync(
    long userId, MovieCreateInput createInput, CancellationToken token = default) {
    Movie movie = new() {
      UserId = userId,
      Title = createInput.Title,
      YearOfRelease = createInput.YearOfRelease
    };

    await using var transaction = await dbContext.Database.BeginTransactionAsync(token);

    try {
      await movieRepo.AddAsync(movie, token);
      await genreRepo.AddRangeAsync(createInput.Genres.Select(name => new Genre {
        MovieId = movie.Id,
        Name = name
      }), token);

      await transaction.CommitAsync(token);
    }
    catch (Exception e) {
      Console.WriteLine(e);
      return null;
    }

    return movie;
  }

  /// <summary>
  /// Updates a movie
  /// </summary>
  /// <param name="userId">The User ID</param>
  /// <param name="input">The input data object</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Whatever the movie is updated or not</returns>
  public async Task<Movie?> UpdateAsync(
    long userId, MovieUpdateInput input, CancellationToken token = default) {
    var movie = await movieRepo.GetOrDefaultAsync(x => x.UserId == userId && x.Id == input.Id, token: token);

    ErrorHelper.ThrowIfNull(movie, "Movie not found");

    var newSlug = movie.GenerateSlug(input.Title, input.YearOfRelease);
    var sameMovieExists =
      !newSlug.Equals(movie.Slug)
      && await movieRepo.ExistsAsync(x => x.Slug == newSlug, token);

    ErrorHelper.ThrowIfTrue(sameMovieExists,
      "Movie with the same name and year of release is already exist.");


    await using var transaction = await dbContext.Database.BeginTransactionAsync(token);

    try {
      // remove existing genres
      await genreRepo.DeleteAsync(x => x.MovieId == movie.Id, token);

      await movieRepo.UpdateAsync(movie, token);
      await genreRepo.AddRangeAsync(input.Genres.Select(name => new Genre {
        MovieId = movie.Id,
        Name = name
      }), token);

      await dbContext.SaveChangesAsync(token);
      await transaction.CommitAsync(token);
    }
    catch (Exception e) {
      Console.WriteLine(e);
      return null;
    }

    return movie;
  }
}