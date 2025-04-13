// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Database.Entities;

namespace Server.Resolvers.Ratings.Mutations;

[MutationType]
public class RatingDeleteMutation {
  [Authorize(Policy = AuthPolicy.Trusted)]
  [GraphQLDescription("Removes the movie rating")]
  public async Task<bool> RatingDeleteAsync(
    [ID(nameof(Movie))]
    long movieId, RatingRepository ratingRepo,
    IHttpContextAccessor contextAccessor, CancellationToken token
  ) {
    var context = contextAccessor.HttpContext!;
    return await ratingRepo.DeleteAsync(r
      => r.UserId == context.GetId() &&
         r.MovieId == movieId, token) > 0;
  }
}