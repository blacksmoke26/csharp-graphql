// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Rating;
using Application.Domain.Validators.Rating;

namespace Server.Resolvers.Ratings.Mutations;

[MutationType]
public class RatingMovieMutation {
  [Authorize(Policy = AuthPolicy.Trusted)]
  [GraphQLDescription("Rates a single movie")]
  public Task<bool> RatingMovieAsync(
    [UseFluentValidation, UseValidator<MovieRatingInputValidator>]
    MovieRatingInput input,
    RatingService ratingService,
    IHttpContextAccessor contextAccessor, CancellationToken token
  ) {
    var context = contextAccessor.HttpContext!;
    return ratingService.RateMovieAsync(context.GetId(), input, token);
  }
}