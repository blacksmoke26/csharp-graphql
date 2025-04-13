// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Movie;

namespace Server.Resolvers.Movies.Filtering;

public class MovieFilterInputType : FilterInputType<MoviePayload> {
  protected override void Configure(IFilterInputTypeDescriptor<MoviePayload> descriptor) {
    descriptor.BindFieldsExplicitly();

    descriptor.Field(x => x.Title);
    descriptor.Field(x => x.Status);
    descriptor.Field(x => x.Rating);
    descriptor.Field(x => x.YearOfRelease);
    descriptor.Field(x => x.UserId);
  }
}