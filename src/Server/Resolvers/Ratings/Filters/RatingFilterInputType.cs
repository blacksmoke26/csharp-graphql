// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Rating;

namespace Server.Resolvers.Ratings.Filters;

public class RatingFilterInputType : FilterInputType<RatingPayload> {
  protected override void Configure(IFilterInputTypeDescriptor<RatingPayload> descriptor) {
    descriptor.BindFieldsExplicitly();
    descriptor.AllowOr(false).AllowAnd(false);

    descriptor.Field(x => x.Score);
    descriptor.Field(x => x.CreatedAt);
    descriptor.Field(x => x.UserId);
  }
}