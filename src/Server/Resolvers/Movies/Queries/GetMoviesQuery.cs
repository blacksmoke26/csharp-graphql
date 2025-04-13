// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql
// Projection: https://stackoverflow.com/questions/70651225/using-projections-without-returning-iqueryable-graphql-hotchocolate

using Abstraction.Payloads.Movie;
using Database.Constants;

namespace Server.Resolvers.Movies.Queries;

[QueryType]
public class GetMoviesQuery {
  [GraphQLDescription("Get movies listing")]
  [UsePaging(IncludeTotalCount = true, RequirePagingBoundaries = true)]
  [UseProjection]
  [UseFiltering]
  public IQueryable<MoviePayload> GetMovies(MovieRepository movieRepo) {
    return movieRepo.GetQueryable(new() {
      Condition = movie => movie.Status == MovieStatus.Published
    }).SelectMoviePayload();
  }
}