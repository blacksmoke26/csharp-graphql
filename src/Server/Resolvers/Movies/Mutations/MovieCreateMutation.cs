// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Movie;
using Abstraction.Payloads.Movie;
using Application.Domain.Validators.Movie;

namespace Server.Resolvers.Movies.Mutations;

[MutationType]
public class MovieCreateMutation {
  [Authorize(Policy = AuthPolicy.Trusted)]
  [UseFirstOrDefault]
  [UseProjection]
  [GraphQLDescription("Creates a single movie")]
  public async Task<IQueryable<MoviePayload>> MovieCreateAsync(
    [UseFluentValidation, UseValidator<MovieCreateInputValidator>]
    MovieCreateInput input,
    MovieService movieService,
    MovieRepository movieRepo,
    IHttpContextAccessor contextAccessor, CancellationToken token
  ) {
    var context = contextAccessor.HttpContext!;

    var movie = await movieService.CreateAsync(context.GetId(), input, token);

    ErrorHelper.ThrowIfNull(movie, "An error occurred while creating the movie");

    return movieRepo.GetQueryable(new() {
      Condition = m => m.Id == movie.Id,
    }).SelectMoviePayload().OrderBy(x => x.Id);
  }
}