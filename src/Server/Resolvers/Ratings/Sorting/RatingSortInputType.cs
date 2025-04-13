// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Rating;
using HotChocolate.Data.Sorting;

namespace Server.Resolvers.Ratings.Sorting;

public class RatingSortInputType : SortInputType<RatingPayload> {
  protected override void Configure(ISortInputTypeDescriptor<RatingPayload> descriptor) {
    descriptor.BindFieldsExplicitly();

    descriptor.Field(x => x.Score);
    descriptor.Field(x => x.MovieId);
    descriptor.Field(x => x.CreatedAt);
    descriptor.Field(x => x.UpdatedAt);
  }
}