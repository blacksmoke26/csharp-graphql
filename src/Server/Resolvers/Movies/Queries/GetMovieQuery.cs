// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql
// Projection: https://stackoverflow.com/questions/70651225/using-projections-without-returning-iqueryable-graphql-hotchocolate

using Abstraction.Payloads.Movie;
using Database.Entities;

namespace Server.Resolvers.Movies.Queries;

[QueryType]
public class GetMovieQuery {
  [GraphQLDescription("Get the movie by ID or slug")]
  [UseFirstOrDefault]
  [UseProjection]
  public IQueryable<MoviePayload> GetMovie(
    [ID(nameof(Movie))]
    long? id, string? slug, MovieRepository movieRepo) {
    ErrorHelper.ThrowIfTrue(id is null && slug is null, "Either one of movie ID or Slug is required");
    ErrorHelper.ThrowIfTrue(id != null && slug != null, "Only one of them (ID or Slug) is required but both are present");

    return movieRepo.GetQueryable(new() {
      Condition = m => slug == null ? m.Id == id : m.Slug == slug,
    }).SelectMoviePayload().OrderBy(x => x.Id);
  }
}