// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Rating;
using Application.Helpers;

namespace Application.Services;

public class RatingService(
  MovieRepository movieRepo,
  RatingRepository ratingRepo
) : ServiceBase {
  /// <summary>Rates the movie against the movie id by the user</summary>
  /// <param name="userId">The user ID</param>
  /// <param name="input">The input object</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Whatever the rating was a success or failure</returns>
  public async Task<bool> RateMovieAsync(long userId,
    MovieRatingInput input, CancellationToken token = default) {
    // Ensure that movie exists, otherwise the constraints exception thrown 
    var isFound = await movieRepo.ExistsAsync(x
      => x.Id == input.MovieId && x.Status != MovieStatus.Published, token);

    ErrorHelper.ThrowIfFalse(isFound, "Movie not found");

    var record = await ratingRepo.GetOrDefaultAsync(x
      => x.UserId == userId && x.MovieId == input.MovieId, token);

    var model = record ?? new Rating();

    model.UserId = userId;
    model.MovieId = input.MovieId;
    model.Score = input.Score;
    model.Feedback = input.Feedback;

    return record is null
      ? await ratingRepo.AddAsync(model, token) > 0
      : await ratingRepo.UpdateAsync(model, token) > 0;
  }
}