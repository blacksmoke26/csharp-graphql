// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Rating;
using Database.Core.Base;
using Database.Entities;
using HotChocolate.Data.Sorting;
using Server.Resolvers.Ratings.Sorting;

namespace Server.Resolvers.Ratings.Queries;

[QueryType]
public class RatingsQuery {
  [GraphQLDescription("Get user ratings")]
  [Authorize(Policy = AuthPolicy.Trusted)]
  [UsePaging(IncludeTotalCount = true, RequirePagingBoundaries = true)]
  [UseProjection]
  [UseFiltering]
  [UseSorting<RatingSortInputType>]
  public IQueryable<RatingPayload> GetRatings(
    RatingRepository ratingRepo, IFilterContext filterContext,
    IHttpContextAccessor contextAccessor, ISortingContext sortingContext
  ) {
    var context = contextAccessor.HttpContext!;

    filterContext.Handled(false);
    sortingContext.Handled(false);

    var filterOptions = new FilterOptions<Rating> {
      Condition = x => x.UserId == context.GetId()
    };

    if (!sortingContext.IsDefined) {
      filterOptions.Order.Add(x => x.CreatedAt, SortOrder.Descending);
    }

    return ratingRepo.GetQueryable(filterOptions).SelectRatingPayload();
  }
}