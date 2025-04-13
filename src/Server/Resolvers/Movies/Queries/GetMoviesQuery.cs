// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql
// Projection: https://stackoverflow.com/questions/70651225/using-projections-without-returning-iqueryable-graphql-hotchocolate

using Abstraction.Payloads.Movie;
using Database.Core.Base;
using Database.Entities;
using HotChocolate.Data.Sorting;
using Server.Resolvers.Movies.Sorting;

namespace Server.Resolvers.Movies.Queries;

[QueryType]
public class GetMoviesQuery {
  [GraphQLDescription("Get movies listing")]
  [UsePaging(IncludeTotalCount = true, RequirePagingBoundaries = true)]
  [UseProjection]
  [UseFiltering]
  [UseSorting<MovieSortInputType>]
  public IQueryable<MoviePayload> GetMovies(
    MovieRepository movieRepo, IFilterContext filterContext, IHttpContextAccessor contextAccessor,
    ISortingContext sortingContext
  ) {
    var context = contextAccessor.HttpContext!;

    filterContext.Handled(false);
    sortingContext.Handled(false);

    var filterOptions = new FilterOptions<Movie>();

    filterOptions.AndWhere.Add(
      !context.IsAdminRole(),
      movie => movie.Status == MovieStatus.Published
    );

    if (!sortingContext.IsDefined) {
      filterOptions.Order.Add(x => x.CreatedAt, SortOrder.Descending);
    }

    return movieRepo.GetQueryable(filterOptions).SelectMoviePayload();
  }
}