// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Movie;
using HotChocolate.Data.Sorting;

namespace Server.Resolvers.Movies.Sorting;

public class MovieSortInputType : SortInputType<MoviePayload> {
  protected override void Configure(ISortInputTypeDescriptor<MoviePayload> descriptor) {
    descriptor.BindFieldsExplicitly();
    descriptor.Field(x => x.CreatedAt);
    descriptor.Field(x => x.Title);
    descriptor.Field(x => x.Rating);
    descriptor.Field(x => x.YearOfRelease);
    //descriptor.Field(x => x.Status).Type<>();
  }
}