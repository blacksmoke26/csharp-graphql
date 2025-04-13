// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Genre;

namespace Server.Resolvers.Genres.Filtering;

public class GenreFilterInputTypeGenreFilterInputType : FilterInputType<GenrePayload> {
  protected override void Configure(IFilterInputTypeDescriptor<GenrePayload> descriptor) {
    descriptor.BindFieldsExplicitly();
    descriptor.Field(x => x.Name);
  }
}