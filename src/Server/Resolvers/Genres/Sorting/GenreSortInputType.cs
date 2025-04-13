// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Abstraction.Payloads.Genre;
using HotChocolate.Data.Sorting;

namespace Server.Resolvers.Genres.Sorting;

public class GenreSortInputType : SortInputType<GenrePayload> {
  protected override void Configure(ISortInputTypeDescriptor<GenrePayload> descriptor) {
    descriptor.BindFieldsExplicitly();
    descriptor.Field(x => x.Name);
  }
}