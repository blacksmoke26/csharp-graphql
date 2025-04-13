// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Database.Entities;

namespace Server.Resolvers.Movies.Mutations;

[MutationType]
public class MovieDeleteMutation {
  [Authorize(Policy = AuthPolicies.AuthPolicy)]
  [GraphQLDescription("Deletes a movie")]
  public async Task<bool> MovieDeleteAsync(
    [ID(nameof(Movie))]
    long movieId,
    MovieRepository movieRepo,
    IHttpContextAccessor contextAccessor, CancellationToken token
  ) {
    var context = contextAccessor.HttpContext!;
    return await movieRepo.DeleteAsync(x => x.UserId == context.GetId() && x.Id == movieId, token) > 0;
  }
}