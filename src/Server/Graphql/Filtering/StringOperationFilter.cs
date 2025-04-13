// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Server.Graphql.Filtering;

public class StringOperationFilter : StringOperationFilterInputType {
  protected override void Configure(IFilterInputTypeDescriptor descriptor) {
    descriptor.Operation(DefaultFilterOperations.StartsWith).Type<StringType>();
    descriptor.Operation(DefaultFilterOperations.Contains).Type<StringType>();
  }
}