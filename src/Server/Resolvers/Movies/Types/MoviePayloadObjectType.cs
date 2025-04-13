// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql
// Guide: https://chillicream.com/docs/hotchocolate/v15/defining-a-schema/object-types

using Abstraction.Payloads.Account;
using Abstraction.Payloads.Genre;
using Abstraction.Payloads.Movie;
using Abstraction.Payloads.Rating;
using Server.Resolvers.Ratings.Filtering;
using Server.Resolvers.Ratings.Sorting;

namespace Server.Resolvers.Movies.Types;

public class MoviePayloadObjectType : ObjectType<MoviePayload> {
  protected override void Configure(IObjectTypeDescriptor<MoviePayload> descriptor) {
    descriptor.Field("user")
      .UseProjection()
      .UseFirstOrDefault()
      .Resolve(IQueryable<UserPayload> (context) => {
        var parent = context.Parent<MoviePayload>();
        return context.Service<UserRepository>()
          .GetQueryable()
          .Where(x => x.Id == parent.UserId)
          .SelectUserPayload();
      });

    descriptor.Field("genres")
      .UseProjection()
      .UseFiltering()
      .Resolve(IQueryable<GenrePayload> (context) => {
        var parent = context.Parent<MoviePayload>();
        return context.Service<GenreRepository>()
          .GetQueryable()
          .Where(x => x.MovieId == parent.Id)
          .SelectGenrePayload().OrderBy(x => x.Name);
      });

    descriptor.Field("ratings")
      .UsePaging(options: new() {
        MaxPageSize = 20,
        DefaultPageSize = 10,
        IncludeTotalCount = true,
        RequirePagingBoundaries = true
      })
      .UseProjection()
      .UseFiltering<RatingFilterInputType>()
      .UseSorting<RatingSortInputType>()
      .Resolve(IQueryable<RatingPayload> (context) => {
        var parent = context.Parent<MoviePayload>();
        return context.Service<RatingRepository>()
          .GetQueryable()
          .Where(x => x.MovieId == parent.Id)
          .SelectRatingPayload();
      });
  }
}